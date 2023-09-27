using System.Collections.ObjectModel;

namespace IAC.Application.Auth;

public static class PermissionPolicy
{
    public static ReadOnlyCollection<PermissionInfo> AllPermissions => s_allPermission.AsReadOnly();
    
    private static readonly List<PermissionInfo> s_allPermission = new() 
    {
        new (Common.ViewDashboard, "View dashboard"),
        new (Common.ViewReports, "View reports"),
        new (Common.ManageSettings, "Manage settings"),

        new (Customer.View, "View customer"),
        new (Customer.Add, "Add customer"),
        new (Customer.Edit, "Edit customer"),
        new (Customer.Delete, "Delete customer"),

        new (Contract.View, "View contract"),
        new (Contract.Add, "Add contract"),
        new (Contract.Edit, "Edit contract"),
        new (Contract.Delete, "Delete contract"),

        new (Installation.View, "View installation"),
        new (Installation.Add, "Add installation"),
        new (Installation.Edit, "Edit installation"),
        new (Installation.Delete, "Delete installation"),

        new (PurchaseOrder.View, "View purchase order"),
        new (PurchaseOrder.Add, "Add purchase order"),
        new (PurchaseOrder.Edit, "Edit purchase order"),
        new (PurchaseOrder.Delete, "Delete purchase order"),

        new (Product.View, "View product"),
        new (Product.Add, "Add product"),
        new (Product.Edit, "Edit product"),
        new (Product.Delete, "Delete product"),

        new (Supplier.View, "View supplier"),
        new (Supplier.Add, "Add supplier"),
        new (Supplier.Edit, "Edit supplier"),
        new (Supplier.Delete, "Delete supplier"),

        new (Inventory.View, "View inventory"),
        new (Inventory.AddStock, "Add stock to inventory"),
    };

    public static class Common
    {
        public const string ViewDashboard = "Common.View.Dashboard";
        public const string ViewReports = "Common.View.Reports";
        public const string ManageSettings = "Common.Manage.Settings";
    }

    public static class Customer
    {
        public const string View = "Customer.View";
        public const string Add = "Customer.Add";
        public const string Edit = "Customer.Edit";
        public const string Delete = "Customer.Delete";
    }

    public static class Contract
    {
        public const string View = "Contract.View";
        public const string Add = "Contract.Add";
        public const string Edit = "Contract.Edit";
        public const string Delete = "Contract.Delete";
    }

    public static class Installation
    {
        public const string View = "Installation.View";
        public const string Add = "Installation.Add";
        public const string Edit = "Installation.Edit";
        public const string Delete = "Installation.Delete";
    }

    public static class PurchaseOrder
    {
        public const string View = "PurchaseOrder.View";
        public const string Add = "PurchaseOrder.Add";
        public const string Edit = "PurchaseOrder.Edit";
        public const string Delete = "PurchaseOrder.Delete";
    }

    public static class Product
    {
        public const string View = "Product.View";
        public const string Add = "Product.Add";
        public const string Edit = "Product.Edit";
        public const string Delete = "Product.Delete";
    }

    public static class Supplier
    {
        public const string View = "Supplier.View";
        public const string Add = "Supplier.Add";
        public const string Edit = "Supplier.Edit";
        public const string Delete = "Supplier.Delete";
    }

    public static class Inventory
    {
        public const string View = "Inventory.View";
        public const string AddStock = "Inventory.AddStock";
    }
}
