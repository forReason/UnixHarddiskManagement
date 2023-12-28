using System;
using System.Text;

namespace UnixHarddiskManagement.Fstab
{
    public class FstabMountingOption
    {
        private FstabMountingOption(string value) { Value = value; }

        public string Value { get; private set; }
        
        public static FstabMountingOption[] GetOptions(string options)
        {
            List<FstabMountingOption> convertedOptions = new List<FstabMountingOption>();
            foreach (string option in options.Split(','))
            {
                convertedOptions.Add(GetOption(option));
            }
            return convertedOptions.ToArray();
        }
        public static string GetOptionString(FstabMountingOption[] options)
        {
            StringBuilder builder = new StringBuilder();
            foreach (FstabMountingOption option in options)
            {
                builder.Append(option.Value);
                builder.Append(',');
            }
            builder.Remove(builder.Length-1,1);
            return builder.ToString();
        }
        /// <summary>
        /// converts a single option string to a mounting option
        /// </summary>
        /// <param name="option">string to be converted</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">selected option could not be matched</exception>
        public static FstabMountingOption GetOption(string option)
        {
            switch (option)
            {
                case "async":
                    return Async;
                case "sync":
                    return Sync;
                case "atime":
                    return Atime;
                case "noatime":
                    return NoAtime;
                case "diratime":
                    return DirATime;
                case "nodiratime":
                    return NoDirATime;
                case "dirsync":
                    return DirSync;
                case "auto":
                    return Auto;
                case "noauto":
                    return Noauto;
                case "defaults":
                    return Defaults;
                case "dev":
                    return Dev;
                case "nodev":
                    return NoDev;
                case "exec":
                    return Exec;
                case "noexec":
                    return NoExec;
                case "group":
                    return Group;
                case "owner":
                    return Owner;
                case "user":
                    return User;
                case "nouser":
                    return NoUser;
                case "users":
                    return Users;
                case "iversion":
                    return IVersion;
                case "noiversion":
                    return NoIVersion;
                case "netdev":
                    return NetDev;
                case "nofail":
                    return Nofail;
                case "suid":
                    return Suid;
                case "nosuid":
                    return NoSuid;
                case "silent":
                    return Silent;
                case "loud":
                    return Loud;
                case "ro":
                    return ReadOnly;
                case "rw":
                    return ReadWrite;
                case "sw":
                    return SwapOn;
                case "errors=continue":
                    return ErrorsContinue;
                // Add any additional cases here
                default:
                    throw new NotImplementedException($"Mounting option '{option}' was not found. It was either not implemented yet or is faulty! Please make a request at GitHub");
            }
        }


        /// <summary>
        ///  All I/O to the filesystem should be done asynchronously. (See also the sync option.)
        /// </summary>
        public static FstabMountingOption Async { get { return new FstabMountingOption("async"); } }
        /// <summary>
        /// All I/O to the filesystem should be done synchronously. 
        /// In the case of media with a limited number of write cycles(e.g.some flash drives), sync may cause life-cycle shortening.
        /// </summary>
        public static FstabMountingOption Sync { get { return new FstabMountingOption("sync"); } }
        /// <summary>
        /// Do not use the noatime feature, so the inode access time is controlled by kernel defaults.See also the descriptions of the relatime and strictatime mount options.
        /// </summary>
        public static FstabMountingOption Atime { get { return new FstabMountingOption("atime"); } }
        /// <summary>
        ///  Do not update inode access times on this filesystem (e.g. for faster access on the
        ///  news spool to speed up news servers). This works for all inode types(directories too), so it implies nodiratime.
        /// </summary>
        public static FstabMountingOption NoAtime { get { return new FstabMountingOption("noatime"); } }
        /// <summary>
        /// Update directory inode access times on this filesystem. This is the default. (This option is ignored when noatime is set.)
        /// </summary>
        public static FstabMountingOption DirATime { get { return new FstabMountingOption("diratime"); } }
        /// <summary>
        /// Do not update directory inode access times on this filesystem. (This option is implied when noatime is set.)
        /// </summary>
        public static FstabMountingOption NoDirATime { get { return new FstabMountingOption("nodiratime"); } }
        /// <summary>
        /// All directory updates within the filesystem should be done synchronously. <br/>
        /// This affects the following system calls: creat(2), link(2), unlink(2), symlink(2), mkdir(2), rmdir(2), mknod(2) and rename(2).
        /// </summary>
        public static FstabMountingOption DirSync { get { return new FstabMountingOption("dirsync"); } }
        /// <summary>
        ///  Can be mounted with the -a option.
        /// </summary>
        public static FstabMountingOption Auto { get { return new FstabMountingOption("auto"); } }
        /// <summary>
        /// Can only be mounted explicitly (i.e., the -a option will not cause the filesystem to be mounted).
        /// </summary>
        public static FstabMountingOption Noauto { get { return new FstabMountingOption("noauto"); } }
        /// <summary>
        ///  Use the default options: rw, suid, dev, exec, auto, nouser, and async.
        /// </summary>
        public static FstabMountingOption Defaults { get { return new FstabMountingOption("defaults"); } }
        /// <summary>
        /// Interpret character or block special devices on the filesystem.
        /// </summary>
        public static FstabMountingOption Dev { get { return new FstabMountingOption("dev"); } }
        /// <summary>
        /// Do not interpret character or block special devices on the filesystem.
        /// </summary>
        public static FstabMountingOption NoDev { get { return new FstabMountingOption("nodev"); } }
        /// <summary>
        /// Permit execution of binaries and other executable files.
        /// </summary>
        public static FstabMountingOption Exec { get { return new FstabMountingOption("exec"); } }
        /// <summary>
        /// Do not permit direct execution of any binaries on the mounted filesystem.
        /// </summary>
        public static FstabMountingOption NoExec { get { return new FstabMountingOption("noexec"); } }
        /// <summary>
        /// Allow an ordinary user to mount the filesystem if one of that user’s groups matches the group of the device.<br/>
        /// This option implies the options nosuid and nodev(unless overridden by subsequent options, as in the option line group, dev, suid).
        /// </summary>
        public static FstabMountingOption Group { get { return new FstabMountingOption("group"); } }
        /// <summary>
        /// Allow an ordinary user to mount the filesystem if that user is the owner of the device.
        /// This option implies the options nosuid and nodev(unless overridden by subsequent options, as in the option line owner, dev, suid).
        /// </summary>
        public static FstabMountingOption Owner { get { return new FstabMountingOption("owner"); } }
        /// <summary>
        /// Allow an ordinary user to mount the filesystem. The name of the mounting user is written to the mtab file (or to the private libmount file in /run/mount on systems
        /// without a regular mtab) so that this same user can unmount the filesystem again.
        /// This option implies the options noexec, nosuid, and nodev(unless overridden by subsequent options, as in the option line user, exec, dev, suid).

        /// </summary>
        public static FstabMountingOption User { get { return new FstabMountingOption("user"); } }
        /// <summary>
        /// Forbid an ordinary user to mount the filesystem. This is the default; it does not imply any other options.
        /// </summary>
        public static FstabMountingOption NoUser { get { return new FstabMountingOption("nouser"); } }
        /// <summary>
        /// Allow any user to mount and to unmount the filesystem, even when some other ordinary user mounted it.
        /// This option implies the options noexec, nosuid, and nodev(unless overridden by subsequent options, as in the option line users, exec, dev, suid).
        /// </summary>
        public static FstabMountingOption Users { get { return new FstabMountingOption("users"); } }
        /// <summary>
        /// Every time the inode is modified, the i_version field will be incremented.
        /// </summary>
        public static FstabMountingOption IVersion { get { return new FstabMountingOption("iversion"); } }
        /// <summary>
        /// Do not increment the i_version inode field.
        /// </summary>
        public static FstabMountingOption NoIVersion { get { return new FstabMountingOption("noiversion"); } }
        /// <summary>
        /// The filesystem resides on a device that requires network access 
        /// (used to prevent the system from attempting to mount these filesystems until the network has been enabled on the system).
        /// </summary>
        public static FstabMountingOption NetDev { get { return new FstabMountingOption("netdev"); } }
        /// <summary>
        /// Do not report errors for this device if it does not exist.
        /// </summary>
        public static FstabMountingOption Nofail { get { return new FstabMountingOption("nofail"); } }
        /// <summary>
        /// Honor set-user-ID and set-group-ID bits or file capabilities when executing programs from this filesystem.
        /// </summary>
        public static FstabMountingOption Suid { get { return new FstabMountingOption("suid"); } }
        /// <summary>
        /// Do not honor set-user-ID and set-group-ID bits or file capabilities when executing programs from this filesystem.
        /// In addition, SELinux domain transitions require permission nosuid_transition, which in turn needs also policy capability nnp_nosuid_transition.
        /// </summary>
        public static FstabMountingOption NoSuid { get { return new FstabMountingOption("nosuid"); } }
        /// <summary>
        /// Turn on the silent flag.
        /// </summary>
        public static FstabMountingOption Silent { get { return new FstabMountingOption("silent"); } }
        /// <summary>
        /// Turn off the silent flag.
        /// </summary>
        public static FstabMountingOption Loud { get { return new FstabMountingOption("loud"); } }
        /// <summary>
        /// Mount the filesystem read-only.
        /// </summary>
        public static FstabMountingOption ReadOnly { get { return new FstabMountingOption("ro"); } }
        /// <summary>
        /// Mount the filesystem read-write.
        /// </summary>
        public static FstabMountingOption ReadWrite { get { return new FstabMountingOption("rw"); } }
        /// <summary>
        /// If fs_type is “sw” then the special file is made available as a piece of swap space by the swapon(8) command at the end of the system reboot procedure.
        /// </summary>
        public static FstabMountingOption SwapOn { get { return new FstabMountingOption("sw"); } }
        public static FstabMountingOption ErrorsContinue { get { return new FstabMountingOption("errors=continue"); } }
    }
}
