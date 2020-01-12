using UnityEngine;
using System.IO.MemoryMappedFiles;

[ModTitle("Mumble Link")] // The mod name.
[ModDescription("Allows positional voice audio via Mumble.")] // Short description for the mod.
[ModAuthor("yaks")] // The author name of the mod.
[ModIconUrl("https://www.logolynx.com/images/logolynx/01/01a38e1b8398e89861c608fe1e7d8d19.jpeg")] // An icon for your mod. Its recommended to be 128x128px and in .jpg format.
[ModWallpaperUrl("Banner Url")] // A banner for your mod. Its recommended to be 330x100px and in .jpg format.
[ModVersionCheckUrl("https://raftmodding.com/api/v1/mods/mumble-link/version.txt")] // This is for update checking. Needs to be a .txt file with the latest mod version.
[ModVersion("v0.2")] // This is the mod version.
[RaftVersion("10.07")] // This is the recommended raft version.
[ModIsPermanent(false)] // If your mod add new blocks, new items or just content you should set that to true. It loads the mod on start and prevents unloading.
public class MumbleLink : Mod
{

    public MemoryMappedFile mapped;
    public MemoryMappedViewAccessor mumble;
    public uint tick;

    // The Start() method is being called when your mod gets loaded.
    public void Start()
    {
        tick = 0;
        RConsole.Log("Mumble Link has been loaded!");
        RConsole.Log("Starting connection to Mumble...");
        GetMumble();
    }

    // The Update() method is being called every frame. Have fun!
    public void Update()
    {
        this.tick++;
        var mum = GetMumble();
        var player = RAPI.GetLocalPlayer();

        if (player == null || mum == null)
        {
            return;
        }
        

        var cam = player.Camera;


        // RConsole.Log("Got player location: " + player.FeetPosition.ToString() + " Camera rotation: " + cam.transform.forward.ToString() + " Camera up: " + cam.transform.up.ToString());

        // UI Version
        mum.Write(0, 2);

        // Tick
        var tick = mum.ReadUInt32(4);
        mum.Write(4, tick + 1);

        // Plugin name
        WriteString(44, "Raft Mumble Link");
        WriteString(1620, "Raft mod to enable positional audio.");

        // uid
        var uid = player.steamID.ToString();
        WriteString(592, uid);
            
        // Context length
        mum.Write(592, 1);

        // Context
        var ctx = player.Network.HostID.ToString();
        WriteString(596, ctx);

        // Character position
        WriteVec(8, cam.transform.position);

        // Unit vector out of character eyes
        WriteVec(20, cam.transform.forward);
        WriteVec(568, cam.transform.forward);

        // Unit vector out of character head
        WriteVec(32, cam.transform.up);
        WriteVec(80, cam.transform.up);

        // Camera position
        WriteVec(556, cam.transform.position);
    }

    // The OnModUnload() method is being called when your mod gets unloaded.
    public void OnModUnload()
    {
        RConsole.Log("MumbleLink has been unloaded!");
        Destroy(gameObject); // Please do not remove that line!
    }

    public MemoryMappedViewAccessor GetMumble()
    {
        if (mumble != null)
        {
            return mumble;
        }

        if (tick % 600 != 0)
        {
            return null;
        }


        try
        {
            mapped = MemoryMappedFile.OpenExisting("MumbleLink", MemoryMappedFileRights.FullControl);

            mumble = mapped.CreateViewAccessor();
            RConsole.Log("Connected to Mumble.");
            return mumble;
        }
        catch (System.IO.FileNotFoundException)
        {
            RConsole.Log("Mumble not found. Checking again in 10 seconds...");
        }

        return null;
    }

    public void WriteString(uint offset, string str)
    {
        var idx = 0;

        foreach (var ch in str.ToCharArray())
        {
            mumble.Write(offset + (idx++ * 2), ch);
        }
    }

    public void WriteVec(uint offset, Vector3 v)
    {
        mumble.Write(offset, v.x);
        mumble.Write(offset + 4, v.y);
        mumble.Write(offset + 8, v.z);
    }
}

/*

using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Explicit)]
struct LinkedMem
{
    [FieldOffset(0)] public uint uiVersion;
    [FieldOffset(4)] public uint uiTick;

    [FieldOffset(8)] public Vector3 fAvatarPosition, fAvatarFront, fAvatarTop;

    [FieldOffset(44)] public fixed char name[256];

    [FieldOffset(556)] public Vector3 fCameraPosition, fCameraFront, fCameraTop;

    [FieldOffset(592)] public fixed char identity[256];

    [FieldOffset(1104)] public uint context_len;
    [FieldOffset(1108)] public fixed char context[256];
    [FieldOffset(1620)] public fixed char description[2048];
}
*/
