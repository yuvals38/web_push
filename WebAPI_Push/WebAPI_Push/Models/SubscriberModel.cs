namespace WebAPI_Push.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SubscriberModel : DbContext
    {
        public SubscriberModel()
            : base("name=pushModel")
        {
        }

        public virtual DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscriber>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.PrivateKey)
                .IsFixedLength();

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.PublicKey)
                .IsFixedLength();

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.Header)
                .IsFixedLength();

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.Message)
                .IsFixedLength();

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.URL)
                .IsFixedLength();

            modelBuilder.Entity<Subscriber>()
                .Property(e => e.Endpoint)
                .IsFixedLength();
        }

        public System.Data.Entity.DbSet<WebAPI_Push.Models.SubscriberData> SubscriberDatas { get; set; }
    }
}
