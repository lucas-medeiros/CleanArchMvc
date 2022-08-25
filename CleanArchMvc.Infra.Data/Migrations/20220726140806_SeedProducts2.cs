using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations;

public partial class SeedProducts2 : Migration
{
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql(
            "INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
            "VALUES('Caderno espiral', 'Caderno espiral 100 folhas', 7.45, 50, 'caderno1.png', 1)"
        );

        mb.Sql(
            "INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
            "VALUES('Estojo escolar', 'Estojo escolar cinza', 5.65, 70, 'estojo1.png', 1)"
        );

        mb.Sql(
            "INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
            "VALUES('Borracha', 'Borracha escolar pequena', 3.25, 80, 'borracha1.png', 1)"
        );

        mb.Sql(
            "INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
            "VALUES('Calculadora', 'Calculadora escolar simples', 15.39, 20, 'calculadora1.png', 2)"
        );
    }

    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("DELETE FROM Products");
    }
}
