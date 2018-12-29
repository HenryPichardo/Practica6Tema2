using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProyectoPropuestoPractica6Tema2.Models
{
    public class RegistroProducto
    {
        private SqlConnection con;
        private void Conectar()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(connStr);
        }
        public int GrabarProducto(Producto prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Insert Into Productos (Descripcion, Tipo, Precio)" +
                                                "Values (@Descripcion, @Tipo, @Precio)", con);
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@Tipo", SqlDbType.VarChar);
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            
            comando.Parameters["@Descripcion"].Value = prod.Descripcion;
            comando.Parameters["@Tipo"].Value = prod.Tipo;
            comando.Parameters["@Precio"].Value = prod.Precio;
            
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public List<Producto> RecuperarTodos()
        {
            Conectar();
            List<Producto> productos = new List<Producto>();

            SqlCommand com = new SqlCommand("Select Id, Descripcion, Tipo, Precio From Productos", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();

            while (registros.Read())
            {
                Producto prod = new Producto
                {
                    Id = int.Parse(registros["Id"].ToString()),
                    Descripcion = registros["Descripcion"].ToString(),
                    Tipo = registros["Tipo"].ToString(),
                    Precio = double.Parse(registros["Precio"].ToString()),

                };
                productos.Add(prod);
            }
            con.Close();
            return productos;

        }
        public Producto Recuperar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Select Id, Descripcion, Tipo, Precio " +
                                                "From Productos where Id=@Id", con);
            comando.Parameters.Add("@Id", SqlDbType.Int);
            comando.Parameters["@Id"].Value = codigo;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Producto producto = new Producto();
            if (registros.Read())
            {
                producto.Descripcion = registros["Descripcion"].ToString();
                producto.Tipo = registros["Tipo"].ToString();
                producto.Precio = double.Parse(registros["Precio"].ToString());
                
            }
            con.Close();
            return producto;
        }
        public int Modificar(Producto prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Update Productos set Descripcion=@Descripcion, Tipo=@Tipo, Precio=@Precio, where Id=@Id", con);

            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters["@Descripcion"].Value = prod.Descripcion;
            comando.Parameters.Add("@Tipo", SqlDbType.VarChar);
            comando.Parameters["@Tipo"].Value = prod.Tipo;
            comando.Parameters.Add("@Precio", SqlDbType.Float);
            comando.Parameters["@Precio"].Value = prod.Precio;
            

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from Productos where Id=@Id", con);
            comando.Parameters.Add("@Id", SqlDbType.Int);
            comando.Parameters["@Id"].Value = codigo;
            con.Open();
            int i = comando.ExecuteNonQuery();
            return i;
        }
    }
}