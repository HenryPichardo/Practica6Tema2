using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Proyecto6Tema2.Models
{
    public class RegistroPelicula
    {
        private SqlConnection con;
        private void Conectar()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(connStr);
        }
        public int GrabarPelicula(Pelicula peli)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Insert Into TBL_PELICULA (Titulo, Director, ActorPrincipal, No_Actores, Duracion, Estreno)" +
                                                "Values (@Titulo, @Director, @ActorPrincipal, @No_Actores, @Duracion, @Estreno)", con);
            comando.Parameters.Add("@Titulo", SqlDbType.VarChar);
            comando.Parameters.Add("@Director", SqlDbType.VarChar);
            comando.Parameters.Add("@ActorPrincipal", SqlDbType.VarChar);
            comando.Parameters.Add("@No_Actores", SqlDbType.Int);
            comando.Parameters.Add("@Duracion", SqlDbType.Float);
            comando.Parameters.Add("@Estreno", SqlDbType.Int);
            comando.Parameters["@Titulo"].Value = peli.Titulo;
            comando.Parameters["@Director"].Value = peli.Director;
            comando.Parameters["@ActorPrincipal"].Value = peli.ActorPrincipal;
            comando.Parameters["@No_Actores"].Value = peli.No_Actores;
            comando.Parameters["@Duracion"].Value = peli.Duracion;
            comando.Parameters["@Estreno"].Value = peli.Estreno;
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;

        }
        public List<Pelicula> RecuperarTodos()
        {
            Conectar();
            List<Pelicula> peliculas = new List<Pelicula>();

            SqlCommand com = new SqlCommand("Select Codigo, Titulo, Director, ActorPrincipal, No_Actores, Duracion, Estreno From TBL_PELICULA", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            
            while (registros.Read())
            {
                Pelicula peli = new Pelicula
                {
                    Codigo = int.Parse(registros["Codigo"].ToString()),
                    Titulo = registros["Titulo"].ToString(),
                    Director = registros["Director"].ToString(),
                    ActorPrincipal = registros["ActorPrincipal"].ToString(),
                    No_Actores = int.Parse(registros["No_Actores"].ToString()),
                    Duracion = double.Parse(registros["Duracion"].ToString()),
                    Estreno = int.Parse(registros["Estreno"].ToString())

                };
                peliculas.Add(peli);
            }
            con.Close();
            return peliculas;

        }
        public Pelicula Recuperar (int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Select Codigo, Titulo, Director, ActorPrincipal, No_Actores, Duracion, Estreno " +
                                                "From TBL_PELICULA where Codigo=@Codigo", con);
            comando.Parameters.Add("@Codigo", SqlDbType.Int);
            comando.Parameters["@Codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            Pelicula pelicula = new Pelicula();
            if (registros.Read())
            {
                pelicula.Titulo = registros["Titulo"].ToString();
                pelicula.Director = registros["Director"].ToString();
                pelicula.ActorPrincipal = registros["ActorPrincipal"].ToString();
                pelicula.No_Actores = int.Parse(registros["No_Actores"].ToString());
                pelicula.Duracion = double.Parse(registros["Duracion"].ToString());
                pelicula.Estreno = int.Parse(registros["Estreno"].ToString());
            }
            con.Close();
            return pelicula;
        }
        public int Modificar (Pelicula peli)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Update TBL_PELICULA set Titulo=@Titulo, Director=@Director, ActorPrincipal=@ActorPrincipal, No_Actores=@No_Actores," +
                "Duracion@Duracion, Estreno@Estreno where Codigo=@Codigo", con);

            comando.Parameters.Add("@Titulo", SqlDbType.VarChar);
            comando.Parameters["@Titulo"].Value = peli.Titulo;
            comando.Parameters.Add("@Director", SqlDbType.VarChar);
            comando.Parameters["@Director"].Value = peli.Director;
            comando.Parameters.Add("@ActorPrincipal", SqlDbType.VarChar);
            comando.Parameters["@ActorPrincipal"].Value = peli.ActorPrincipal;
            comando.Parameters.Add("@No_Actores", SqlDbType.Int);
            comando.Parameters["@No_Actores"].Value = peli.No_Actores;
            comando.Parameters.Add("@Duracion", SqlDbType.Float);
            comando.Parameters["@Duracion"].Value = peli.Titulo;
            comando.Parameters.Add("@Estreno", SqlDbType.Int);
            comando.Parameters["@Estreno"].Value = peli.Estreno;

            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("delete from TBL_PELICULA where Codigo=@Codigo", con);
            comando.Parameters.Add("@Codigo", SqlDbType.Int);
            comando.Parameters["@Codigo"].Value = codigo;
            con.Open();
            int i = comando.ExecuteNonQuery();
            return i;
        }

    }
}