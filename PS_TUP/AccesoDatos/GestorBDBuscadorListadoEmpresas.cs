using PS_TUP.Models.TipoEmpresa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PS_TUP.AccesoDatos
{
    public class GestorBDBuscadorListadoEmpresas
    {

        public static List<Empresas> ObtenerEmpresasListado()
        {
            List<Empresas> resultado = new List<Empresas>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idEmpresa, UserName, CUIT, razonSocial, nombreComercial, p.nombreProvincia, ciudad, calle, " +
                    " altura, piso, paginaWeb, logo, comprobanteInsc, verificada FROM tipoEmpresa t, Provincias p " +
                    " WHERE t.idProvincia = p.idProvincia AND verificada = 'Si' ORDER BY nombreComercial";

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
                        Empresas e = new Empresas();
                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        e.UserName = dr["UserName"].ToString();
                        e.CUIT = dr["CUIT"].ToString();
                        e.razonSocial = dr["razonSocial"].ToString();
                        e.nombreComercial = dr["nombreComercial"].ToString();
                        e.provincia = dr["nombreProvincia"].ToString();
                        e.ciudad = dr["ciudad"].ToString();
                        e.calle = dr["calle"].ToString();
                        e.altura = dr["altura"].ToString();
                        e.piso = dr["piso"].ToString();
                        e.paginaWeb = dr["paginaWeb"].ToString();
                        //e.logo = (byte[])dr["logo"];
                        e.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

                        resultado.Add(e);

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


        public static List<Empresas> ObtenerEmpresasNoVerificadas()
        {
            List<Empresas> resultado = new List<Empresas>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idEmpresa, UserName, CUIT, razonSocial, nombreComercial, p.nombreProvincia, ciudad, calle, " +
                    " altura, piso, paginaWeb, logo, comprobanteInsc, verificada FROM tipoEmpresa t, Provincias p " +
                    " WHERE t.idProvincia = p.idProvincia AND verificada = 'No' ORDER BY nombreComercial";

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
                        Empresas e = new Empresas();
                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        e.UserName = dr["UserName"].ToString();
                        e.CUIT = dr["CUIT"].ToString();
                        e.razonSocial = dr["razonSocial"].ToString();
                        e.nombreComercial = dr["nombreComercial"].ToString();
                        e.provincia = dr["nombreProvincia"].ToString();
                        e.ciudad = dr["ciudad"].ToString();
                        e.calle = dr["calle"].ToString();
                        e.altura = dr["altura"].ToString();
                        e.piso = dr["piso"].ToString();
                        e.paginaWeb = dr["paginaWeb"].ToString();
                        //e.logo = (byte[])dr["logo"];
                        e.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

                        resultado.Add(e);

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



        public static List<Empresas> BuscarEmpresasNoVerificadas(string nombreComercial)
        {
            List<Empresas> resultado = new List<Empresas>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idEmpresa, UserName, CUIT, razonSocial, nombreComercial, p.nombreProvincia, ciudad, calle, " +
                    " altura, piso, paginaWeb, logo, comprobanteInsc, verificada FROM tipoEmpresa t, Provincias p " +
                    " WHERE t.idProvincia = p.idProvincia AND nombreComercial LIKE @nombreComercial COLLATE Latin1_general_CI_AI " +
                    " AND verificada = 'No' ORDER BY nombreComercial";

                cmd.Parameters.Clear();

                cmd.Parameters.Add("@nombreComercial", SqlDbType.VarChar);
                cmd.Parameters["@nombreComercial"].Value = "%" + nombreComercial + "%";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Empresas e = new Empresas();
                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        e.UserName = dr["UserName"].ToString();
                        e.CUIT = dr["CUIT"].ToString();
                        e.razonSocial = dr["razonSocial"].ToString();
                        e.nombreComercial = dr["nombreComercial"].ToString();
                        e.provincia = dr["nombreProvincia"].ToString();
                        e.ciudad = dr["ciudad"].ToString();
                        e.calle = dr["calle"].ToString();
                        e.altura = dr["altura"].ToString();
                        e.piso = dr["piso"].ToString();
                        e.paginaWeb = dr["paginaWeb"].ToString();
                        //e.logo = (byte[])dr["logo"];
                        e.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

                        resultado.Add(e);

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



        public static List<Empresas> BuscarEmpresasVerificadas(string nombreComercial)
        {
            List<Empresas> resultado = new List<Empresas>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idEmpresa, UserName, CUIT, razonSocial, nombreComercial, p.nombreProvincia, ciudad, calle, " +
                    " altura, piso, paginaWeb, logo, comprobanteInsc, verificada FROM tipoEmpresa t, Provincias p " +
                    " WHERE t.idProvincia = p.idProvincia AND nombreComercial LIKE @nombreComercial COLLATE Latin1_general_CI_AI " +
                    " AND verificada = 'Si' ORDER BY nombreComercial";

                cmd.Parameters.Clear();

                cmd.Parameters.Add("@nombreComercial", SqlDbType.VarChar);
                cmd.Parameters["@nombreComercial"].Value = "%" + nombreComercial + "%";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Empresas e = new Empresas();
                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        e.UserName = dr["UserName"].ToString();
                        e.CUIT = dr["CUIT"].ToString();
                        e.razonSocial = dr["razonSocial"].ToString();
                        e.nombreComercial = dr["nombreComercial"].ToString();
                        e.provincia = dr["nombreProvincia"].ToString();
                        e.ciudad = dr["ciudad"].ToString();
                        e.calle = dr["calle"].ToString();
                        e.altura = dr["altura"].ToString();
                        e.piso = dr["piso"].ToString();
                        e.paginaWeb = dr["paginaWeb"].ToString();
                        //e.logo = (byte[])dr["logo"];
                        e.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

                        resultado.Add(e);

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

        public static Empresas ObtenerEmpresa(int idEmpresa)
        {
            Empresas e = new Empresas();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idEmpresa, UserName, CUIT, razonSocial, nombreComercial, p.nombreProvincia, ciudad, calle, " +
                    " altura, piso, paginaWeb, logo, comprobanteInsc, verificada FROM tipoEmpresa t, Provincias p " +
                    " WHERE t.idProvincia = p.idProvincia AND idEmpresa = @idEmpresa ";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        
                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        e.UserName = dr["UserName"].ToString();
                        e.CUIT = dr["CUIT"].ToString();
                        e.razonSocial = dr["razonSocial"].ToString();
                        e.nombreComercial = dr["nombreComercial"].ToString();
                        e.provincia = dr["nombreProvincia"].ToString();
                        e.ciudad = dr["ciudad"].ToString();
                        e.calle = dr["calle"].ToString();
                        e.altura = dr["altura"].ToString();
                        e.piso = dr["piso"].ToString();
                        e.paginaWeb = dr["paginaWeb"].ToString();
                        //e.logo = (byte[])dr["logo"];
                        e.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

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

            return e;
        }


        public static Empresas ObtenerComprobanteModelo(int idEmpresa)
        {
            Empresas e = new Empresas();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idEmpresa, comprobanteInsc, verificada FROM tipoEmpresa t " +
                    " WHERE idEmpresa = @idEmpresa ";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());                
                        e.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

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

            return e;
        }


        public static List<Empresas> ObtenerEmpresasBuscador(string nombreComercial)
        {
            List<Empresas> resultado = new List<Empresas>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "   SELECT idEmpresa, UserName, CUIT, razonSocial, nombreComercial, p.nombreProvincia, ciudad, calle, " +
                    " altura, piso, paginaWeb, logo, comprobanteInsc, verificada FROM tipoEmpresa t, Provincias p " +
                    " WHERE t.idProvincia = p.idProvincia AND nombreComercial LIKE @nombreComercial COLLATE Latin1_general_CI_AI " +
                    " AND verificada = 'Si' ORDER BY nombreComercial";

                cmd.Parameters.Clear();
                cmd.Parameters.Add ("@nombreComercial", SqlDbType.VarChar);
                cmd.Parameters["@nombreComercial"].Value = "%" + nombreComercial + "%";

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        Empresas e = new Empresas();
                        e.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        e.UserName = dr["UserName"].ToString();
                        e.CUIT = dr["CUIT"].ToString();
                        e.razonSocial = dr["razonSocial"].ToString();
                        e.nombreComercial = dr["nombreComercial"].ToString();
                        e.provincia = dr["nombreProvincia"].ToString();
                        e.ciudad = dr["ciudad"].ToString();
                        e.calle = dr["calle"].ToString();
                        e.altura = dr["altura"].ToString();
                        e.piso = dr["piso"].ToString();
                        e.paginaWeb = dr["paginaWeb"].ToString();
                        //e.logo = (byte[])dr["logo"];
                        // resultado.comprobanteInsc = (byte[])dr["comprobanteInsc"];
                        e.verificada = dr["verificada"].ToString();

                        resultado.Add(e);

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

   
        ///////////////////////////////////////////////////////////////////////////
      
        public static bool QuitarVerificacion(int idEmpresa)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE tipoEmpresa SET verificada = 'No' WHERE idEmpresa = @idEmpresa";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);

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


        public static bool VerificarEmpresa(int idEmpresa)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE tipoEmpresa SET verificada = 'Si' WHERE idEmpresa = @idEmpresa";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idEmpresa", idEmpresa);

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