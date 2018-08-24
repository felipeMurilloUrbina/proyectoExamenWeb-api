namespace Proyecto.Examen.WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Agregapermiso : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Roles", "Name", c => c.String(nullable: false));
        }
    }
}
