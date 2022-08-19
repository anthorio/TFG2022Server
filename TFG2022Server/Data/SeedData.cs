using Microsoft.EntityFrameworkCore;
using TFG2022Server.Entities;

namespace TFG2022Server.Data
{
    public static class SeedData
    {
        public static void AddUsuarioData(ModelBuilder modelBuilder)
        {
            //Add Cliente Job Titles
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                UsuarioId = 1,
                Rol = "cliente",
                Email = "sdasdf@sdkfjsd.com",
                Contraseña = "9shjdc78",
                Nombre = "Alberto",
                Apellidos = "Perico",
                Telefono = "123123123",
                Dni = "12457896G",
                FechaNacimiento = DateTime.Now,

            });
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                UsuarioId = 2,
                Rol = "cliente",
                Email = "askjdhasdj@youtc,on",
                Contraseña = "asdasd",
                Nombre = "pepep",
                Apellidos = "lololol",
                Telefono = "678867678",
                Dni = "14728539M",
                FechaNacimiento = DateTime.Parse("10 Feb 1974"),

            });
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                UsuarioId = 3,
                Rol = "Jefe",
                Email = "234980@fj.ocm",
                Contraseña = "312789adshjk",
                Nombre = "Yoigo",
                Apellidos = "Frefre",
                Telefono = "123789798",
                Dni = "123890756D",
                FechaNacimiento = DateTime.Parse("28 Feb 2000"),

            });
            //Add Clientes
            //Sales Manager
            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                ClienteId = 1,
                Direccion = "Nerja noseque nosecuanto",
                Poblacion = "Nerja",
                CodigoPostal = "19285",
                Descuento = 20,
                UsuarioIdCliente = 1,

            });
            //Team Leaders
            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                ClienteId = 2,
                Direccion = "Servilla cale noseweqe avenida tu",
                Poblacion = "Torre",
                CodigoPostal = "79852",
                Descuento = 50,
                UsuarioIdCliente = 2,

            });
            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                ClienteId = 3,
                Direccion = "Torremolainos bar misisipi",
                Poblacion = "Torremolinos",
                CodigoPostal = "59267",
                Descuento = 10,
                UsuarioIdCliente = 3,

            });


        }
    }
}
