namespace UnixHarddiskManagement.Fstab
{
    public enum FstabPassOption
    {
        NoCheck=0, // do not check this file system (eg loop, ramdisk)
        RootDrive=1, // root devices be checked first, all in parallel
        Sequential=2 // all other drives to be checked after root, in sequential order
    }
}
