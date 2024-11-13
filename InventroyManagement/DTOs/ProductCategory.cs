using System.ComponentModel;

namespace InventroyManagement.DTOs
{
    public enum ProductCategory
    {
        [Description("Desktop Computers")]
        DesktopComputers,

        [Description("Laptop Computers")]
        LaptopComputers,

        [Description("Computer Accessories")]
        ComputerAccessories,

        [Description("Printers and Scanners")]
        PrintersAndScanners,

        [Description("Networking Equipment")]
        NetworkingEquipment,

        [Description("Monitors")]
        Monitors,

        [Description("Keyboards")]
        Keyboards,

        [Description("Mice")]
        Mice,

        [Description("Printers")]
        Printers,

        [Description("Networking Devices")]
        NetworkingDevices,

        [Description("Storage Devices")]
        StorageDevices,

        [Description("Graphics Cards")]
        GraphicsCards,

        [Description("Processors")]
        Processors,

        [Description("Motherboards")]
        Motherboards,

        [Description("RAM Modules")]
        RAMModules,

        [Description("Power Supplies")]
        PowerSupplies,

        [Description("Cases")]
        Cases,

        [Description("Cooling Systems")]
        CoolingSystems
    }



}