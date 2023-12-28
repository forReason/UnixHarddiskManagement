using System.Runtime.InteropServices;
using UnixHarddiskManagement.Enumerators;
using UnixHarddiskManagement.Fstab;
using UnixHarddiskManagement.Objects;

namespace UnixHarddiskManagement.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void TestGetFSTab() {
            HarddiskManager manager = new HarddiskManager(host: "testrig",username: "testuser", password: "foobar", keyFile: null);
            FstabFile file = manager.GetFSTab();
            Assert.NotNull(file);
            Assert.True(file.Entries.Count > 0);
        }
        [Fact]
        public void TestGetFileSystems()
        {
            HarddiskManager manager = new HarddiskManager(host: "testrig", username: "testuser", password: "foobar", keyFile: null);
            var fileSystems = manager.GetAllFileSystems();
            { }
        }
        [Fact]
        public void TestGetPartitions()
        {
            HarddiskManager manager = new HarddiskManager(host: "testrig", username: "testuser", password: "foobar", keyFile: null);
            FileSystemFilter filter = new FileSystemFilter();
            filter.DeviceTypes = [DeviceType.Partition];
            var fileSystems = manager.GetFileSystems(filter);
            { }
        }
        [Fact]
        public void TestGetDrives()
        {
            HarddiskManager manager = new HarddiskManager(host: "testrig", username: "testuser", password: "foobar", keyFile: null);
            FileSystemFilter filter = new FileSystemFilter();
            filter.DeviceTypes = [DeviceType.Disk];
            var fileSystems = manager.GetFileSystems(filter);
            { }
        }
        [Fact]
        public void TestGetSmallPartitions()
        {
            HarddiskManager manager = new HarddiskManager(host: "testrig", username: "testuser", password: "foobar", keyFile: null);
            FileSystemFilter filter = new FileSystemFilter();
            filter.DeviceTypes = [DeviceType.Partition];
            filter.AvailableSizeMaximum = new ByteSize(SizeUnit.gb, 40);
            var fileSystems = manager.GetFileSystems(filter);
            { }
        }
        [Fact]
        public void TestGetLargeDrives()
        {
            HarddiskManager manager = new HarddiskManager(host: "testrig", username: "testuser", password: "foobar", keyFile: null);
            FileSystemFilter filter = new FileSystemFilter();
            filter.DeviceTypes = [DeviceType.Disk];
            filter.AvailableSizeMinimum = new ByteSize(SizeUnit.gb, 40);
            var fileSystems = manager.GetFileSystems(filter);
            { }
        }
    }
}
 