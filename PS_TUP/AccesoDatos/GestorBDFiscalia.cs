using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using PS_TUP.Models.GestionFiscalia;

namespace PS_TUP.AccesoDatos
{
    public class GestorBDFiscalia
    {

        public static List<DenunciaDesdeFiscalia> ObtenerDenunciasAbiertas()
        {
            List<DenunciaDesdeFiscalia> resultado = new List<DenunciaDesdeFiscalia>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "  SELECT d.IdDenuncia, d.IdTipoDenuncia, d.Descripcion, d.Foto1, d.Foto2, d.Archivo, d.Fiscalia, " +
                    " d.UserName, d.Fecha, d.activa, t.id_usuario, t.DNI, t.nombre, t.apellido, t.fecha_nac, t.sexo ,t.telefono,p.nombreProvincia, " +
                    " t.ciudad,t.calle,t.altura,t.piso, u.Email FROM Denuncias d, tipoUsuario t, Usuarios u, Provincias p " +
                    " WHERE d.UserName = u.UserName AND t.UserName = d.UserName AND t.UserName = u.UserName AND t.idProvincia = p.idProvincia " +
                    " AND d.activa = 'Abierta' ORDER BY d.Fecha DESC ";

                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        DenunciaDesdeFiscalia d = new DenunciaDesdeFiscalia();
                        d.IdDenuncia = int.Parse(dr["IdDenuncia"].ToString());
                        d.IdTipoDenuncia = dr["IdTipoDenuncia"].ToString();
                        d.Descripcion = dr["Descripcion"].ToString();
                        d.Foto1 = (byte[])dr["Foto1"];
                        d.Foto2 = (byte[])dr["Foto2"];
                        d.Archivo = (byte[])dr["Archivo"];
                        d.Fiscalia = dr["Fiscalia"].ToString();
                        d.UserName = dr["UserName"].ToString();
                        d.Fecha = DateTime.Parse(dr["Fecha"].ToString());
                        d.activa = dr["activa"].ToString();
                        d.id_usuario = int.Parse(dr["id_usuario"].ToString());
                        d.DNI = dr["DNI"].ToString();
                        d.nombre = dr["nombre"].ToString();
                        d.apellido = dr["apellido"].ToString();
                        d.fecha_nac = DateTime.Parse(dr["fecha_nac"].ToString());
                        d.sexo = dr["sexo"].ToString();
                        d.telefono = dr["telefono"].ToString();
                        d.nombreProvincia = dr["nombreProvincia"].ToString();
                        d.ciudad = dr["ciudad"].ToString();
                        d.calle = dr["calle"].ToString();
                        d.altura = dr["altura"].ToString();
                        d.piso = dr["piso"].ToString();
                        d.Email = dr["Email"].ToString();


                        resultado.Add(d);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }


        public static List<DenunciaDesdeFiscalia> ObtenerDenunciasCerradas()
        {
            List<DenunciaDesdeFiscalia> resultado = new List<DenunciaDesdeFiscalia>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "  SELECT d.IdDenuncia, d.IdTipoDenuncia, d.Descripcion, d.Foto1, d.Foto2, d.Archivo, d.Fiscalia, " +
                    " d.UserName, d.Fecha, d.activa, t.id_usuario, t.DNI, t.nombre, t.apellido, t.fecha_nac, t.sexo ,t.telefono,p.nombreProvincia, " +
                    " t.ciudad,t.calle,t.altura,t.piso, u.Email FROM Denuncias d, tipoUsuario t, Usuarios u, Provincias p " +
                    " WHERE d.UserName = u.UserName AND t.UserName = d.UserName AND t.idProvincia = p.idProvincia AND d.activa = 'Cerrada' ORDER BY d.Fecha DESC";

                cmd.Parameters.Clear();

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        DenunciaDesdeFiscalia d = new DenunciaDesdeFiscalia();
                        d.IdDenuncia = int.Parse(dr["IdDenuncia"].ToString());
                        d.IdTipoDenuncia = dr["IdTipoDenuncia"].ToString();
                        d.Descripcion = dr["Descripcion"].ToString();
                        d.Foto1 = (byte[])dr["Foto1"];
                        d.Foto2 = (byte[])dr["Foto2"];
                        d.Archivo = (byte[])dr["Archivo"];
                        d.Fiscalia = dr["Fiscalia"].ToString();
                        d.UserName = dr["UserName"].ToString();
                        d.Fecha = DateTime.Parse(dr["Fecha"].ToString());
                        d.activa = dr["activa"].ToString();
                        d.id_usuario = int.Parse(dr["id_usuario"].ToString());
                        d.DNI = dr["DNI"].ToString();
                        d.nombre = dr["nombre"].ToString();
                        d.apellido = dr["apellido"].ToString();
                        d.fecha_nac = DateTime.Parse(dr["fecha_nac"].ToString());
                        d.sexo = dr["sexo"].ToString();
                        d.telefono = dr["telefono"].ToString();
                        d.nombreProvincia = dr["nombreProvincia"].ToString();
                        d.ciudad = dr["ciudad"].ToString();
                        d.calle = dr["calle"].ToString();
                        d.altura = dr["altura"].ToString();
                        d.piso = dr["piso"].ToString();
                        d.Email = dr["Email"].ToString();


                        resultado.Add(d);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }


        public static DenunciaDesdeFiscalia ObtenerDenunciaPorId(int IdDenuncia)
        {
            DenunciaDesdeFiscalia d = new DenunciaDesdeFiscalia();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();

                string consulta = "SELECT d.IdDenuncia, d.IdTipoDenuncia, d.Descripcion, d.Foto1, d.Foto2, d.Archivo, d.Fiscalia, " +
                 " d.UserName, d.Fecha, d.activa, t.id_usuario, t.DNI, t.nombre, t.apellido, t.fecha_nac, t.sexo ,t.telefono,p.nombreProvincia, " +
                 " t.ciudad,t.calle,t.altura,t.piso, u.Email, d.Audio1, d.Audio2 " +
                 " FROM Denuncias d, tipoUsuario t, Usuarios u, Provincias p " +
                 " WHERE d.UserName = u.UserName AND t.UserName = d.UserName AND t.idProvincia = p.idProvincia " +
                 " AND d.IdDenuncia = @IdDenuncia ORDER BY d.Fecha DESC";


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdDenuncia", IdDenuncia);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        d.IdDenuncia = int.Parse(dr["IdDenuncia"].ToString());
                        d.IdTipoDenuncia = dr["IdTipoDenuncia"].ToString();
                        d.Descripcion = dr["Descripcion"].ToString();
                        d.Foto1 = (byte[])dr["Foto1"];
                        d.Foto2 = (byte[])dr["Foto2"];
                        d.Archivo = (byte[])dr["Archivo"];
                        d.Audio1 = (byte[])dr["Audio1"];
                        d.Audio2 = (byte[])dr["Audio2"];
                        d.Fiscalia = dr["Fiscalia"].ToString();
                        d.UserName = dr["UserName"].ToString();
                        d.Fecha = DateTime.Parse(dr["Fecha"].ToString());
                        d.activa = dr["activa"].ToString();
                        d.id_usuario = int.Parse(dr["id_usuario"].ToString());
                        d.DNI = dr["DNI"].ToString();
                        d.nombre = dr["nombre"].ToString();
                        d.apellido = dr["apellido"].ToString();
                        d.fecha_nac = DateTime.Parse(dr["fecha_nac"].ToString());
                        d.sexo = dr["sexo"].ToString();
                        d.telefono = dr["telefono"].ToString();
                        d.nombreProvincia = dr["nombreProvincia"].ToString();
                        d.ciudad = dr["ciudad"].ToString();
                        d.calle = dr["calle"].ToString();
                        d.altura = dr["altura"].ToString();
                        d.piso = dr["piso"].ToString();
                        d.Email = dr["Email"].ToString();

                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

            return d;
        }


        public static List<DenunciaDesdeFiscalia> ObtenerDenunciasAbiertasPorDNI(string DNI)
        {
            List<DenunciaDesdeFiscalia> resultado = new List<DenunciaDesdeFiscalia>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "  SELECT d.IdDenuncia, d.IdTipoDenuncia, d.Descripcion, d.Foto1, d.Foto2, d.Archivo, d.Fiscalia, " +
                    " d.UserName, d.Fecha, d.activa, t.id_usuario, t.DNI, t.nombre, t.apellido, t.fecha_nac, t.sexo ,t.telefono,p.nombreProvincia, " +
                    " t.ciudad,t.calle,t.altura,t.piso, u.Email FROM Denuncias d, tipoUsuario t, Usuarios u, Provincias p " +
                    " WHERE d.UserName = u.UserName AND t.UserName = d.UserName AND t.idProvincia = p.idProvincia AND d.activa = 'Abierta' " +
                    " AND t.DNI = @DNI ORDER BY d.Fecha DESC";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DNI", DNI);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        DenunciaDesdeFiscalia d = new DenunciaDesdeFiscalia();
                        d.IdDenuncia = int.Parse(dr["IdDenuncia"].ToString());
                        d.IdTipoDenuncia = dr["IdTipoDenuncia"].ToString();
                        d.Descripcion = dr["Descripcion"].ToString();
                        d.Foto1 = (byte[])dr["Foto1"];
                        d.Foto2 = (byte[])dr["Foto2"];
                        d.Archivo = (byte[])dr["Archivo"];
                        d.Fiscalia = dr["Fiscalia"].ToString();
                        d.UserName = dr["UserName"].ToString();
                        d.Fecha = DateTime.Parse(dr["Fecha"].ToString());
                        d.activa = dr["activa"].ToString();
                        d.id_usuario = int.Parse(dr["id_usuario"].ToString());
                        d.DNI = dr["DNI"].ToString();
                        d.nombre = dr["nombre"].ToString();
                        d.apellido = dr["apellido"].ToString();
                        d.fecha_nac = DateTime.Parse(dr["fecha_nac"].ToString());
                        d.sexo = dr["sexo"].ToString();
                        d.telefono = dr["telefono"].ToString();
                        d.nombreProvincia = dr["nombreProvincia"].ToString();
                        d.ciudad = dr["ciudad"].ToString();
                        d.calle = dr["calle"].ToString();
                        d.altura = dr["altura"].ToString();
                        d.piso = dr["piso"].ToString();
                        d.Email = dr["Email"].ToString();


                        resultado.Add(d);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }


        public static List<DenunciaDesdeFiscalia> ObtenerDenunciasCerradasPorDNI(string DNI)
        {
            List<DenunciaDesdeFiscalia> resultado = new List<DenunciaDesdeFiscalia>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "  SELECT d.IdDenuncia, d.IdTipoDenuncia, d.Descripcion, d.Foto1, d.Foto2, d.Archivo, d.Fiscalia, " +
                    " d.UserName, d.Fecha, d.activa, t.id_usuario, t.DNI, t.nombre, t.apellido, t.fecha_nac, t.sexo ,t.telefono,p.nombreProvincia, " +
                    " t.ciudad,t.calle,t.altura,t.piso, u.Email FROM Denuncias d, tipoUsuario t, Usuarios u, Provincias p " +
                    " WHERE d.UserName = u.UserName AND t.UserName = d.UserName AND t.idProvincia = p.idProvincia AND d.activa = 'Cerrada' " +
                    " AND t.DNI = @DNI ORDER BY d.Fecha DESC";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DNI", DNI);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        DenunciaDesdeFiscalia d = new DenunciaDesdeFiscalia();
                        d.IdDenuncia = int.Parse(dr["IdDenuncia"].ToString());
                        d.IdTipoDenuncia = dr["IdTipoDenuncia"].ToString();
                        d.Descripcion = dr["Descripcion"].ToString();
                        d.Foto1 = (byte[])dr["Foto1"];
                        d.Foto2 = (byte[])dr["Foto2"];
                        d.Archivo = (byte[])dr["Archivo"];
                        d.Fiscalia = dr["Fiscalia"].ToString();
                        d.UserName = dr["UserName"].ToString();
                        d.Fecha = DateTime.Parse(dr["Fecha"].ToString());
                        d.activa = dr["activa"].ToString();
                        d.id_usuario = int.Parse(dr["id_usuario"].ToString());
                        d.DNI = dr["DNI"].ToString();
                        d.nombre = dr["nombre"].ToString();
                        d.apellido = dr["apellido"].ToString();
                        d.fecha_nac = DateTime.Parse(dr["fecha_nac"].ToString());
                        d.sexo = dr["sexo"].ToString();
                        d.telefono = dr["telefono"].ToString();
                        d.nombreProvincia = dr["nombreProvincia"].ToString();
                        d.ciudad = dr["ciudad"].ToString();
                        d.calle = dr["calle"].ToString();
                        d.altura = dr["altura"].ToString();
                        d.piso = dr["piso"].ToString();
                        d.Email = dr["Email"].ToString();


                        resultado.Add(d);
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return resultado;
        }



        public static bool CerrarDenuncia(int IdDenuncia)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Denuncias SET activa = 'Cerrada' WHERE IdDenuncia = @IdDenuncia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdDenuncia", IdDenuncia);
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }


            return resultado;
        }


        public static bool ReabrirDenuncia(int IdDenuncia)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Denuncias SET activa = 'Abierta' WHERE IdDenuncia = @IdDenuncia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdDenuncia", IdDenuncia);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;
                cmd.ExecuteNonQuery();
                resultado = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }


            return resultado;
        }


    }
}