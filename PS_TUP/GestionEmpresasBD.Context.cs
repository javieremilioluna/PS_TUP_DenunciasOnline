﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PS_TUP
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PS_TUPEntitiesGestionEmpresas : DbContext
    {
        public PS_TUPEntitiesGestionEmpresas()
            : base("name=PS_TUPEntitiesGestionEmpresas")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TelefonosEmpresas> TelefonosEmpresas { get; set; }
        public virtual DbSet<RedesSocEmpresas> RedesSocEmpresas { get; set; }
        public virtual DbSet<MailsEmpresas> MailsEmpresas { get; set; }
        public virtual DbSet<SorteosEmpresas> SorteosEmpresas { get; set; }
    }
}