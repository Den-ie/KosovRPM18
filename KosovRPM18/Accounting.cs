//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KosovRPM18
{
    using System;
    using System.Collections.Generic;
    
    public partial class Accounting
    {
        public int PointNumber { get; set; }
        public Nullable<System.DateTime> TransferDate { get; set; }
        public string RecieverOrg { get; set; }
        public string OrgAdress { get; set; }
        public Nullable<bool> CommercialOrg { get; set; }
        public string TransferType { get; set; }
        public Nullable<int> TransferSumm { get; set; }
    }
}