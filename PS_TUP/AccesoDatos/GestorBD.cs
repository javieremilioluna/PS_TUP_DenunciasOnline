using PS_TUP.Models;
using PS_TUP.Models.Acceso;
using PS_TUP.Models.DTO;
using PS_TUP.Models.GestionUsuarios;
using PS_TUP.Models.TipoEmpresa;
using PS_TUP.Models.TiposUsuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PS_TUP.AccesoDatos
{
    public class GestorBD
    {

        public static bool ActualizarContraseña(Contraseña c)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Usuarios SET password = @password WHERE userName = @userName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@password", c.Password);
                cmd.Parameters.AddWithValue("@userName", c.UserName);


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


        public static bool ActualizarEmail(Email e)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE Usuarios SET Email = @Email WHERE userName = @userName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Email", e.Mail);
                cmd.Parameters.AddWithValue("@userName", e.UserName);


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


        public static List<Provincias> ObtenerProvincias()
        {
            List<Provincias> resultado = new List<Provincias>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT idProvincia, nombreProvincia FROM provincias";
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
                        Provincias p = new Provincias();
                        p.idProvincia = int.Parse(dr["idProvincia"].ToString());
                        p.nombreProvincia = dr["nombreProvincia"].ToString();

                        resultado.Add(p);
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


        public static List<MotivosDenuncias> ObtenerMotivosDenuncias()
        {
            List<MotivosDenuncias> resultado = new List<MotivosDenuncias>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT IdTipoDenuncia, DescripcionMotivo FROM MotivosDenuncias";
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
                        MotivosDenuncias m = new MotivosDenuncias();
                        m.IdTipoDenuncia = int.Parse(dr["IdTipoDenuncia"].ToString());
                        m.DescripcionMotivo = dr["DescripcionMotivo"].ToString();

                        resultado.Add(m);
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


        ////////////////////////////////////////


        public static RegistroTipoUsuario ObtenerUsuario(string UserName)
        {
            RegistroTipoUsuario resultado = new RegistroTipoUsuario();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT id_usuario, UserName, DNI, nombre, apellido,fecha_nac , sexo ,telefono, idProvincia, ciudad, calle, altura, piso, datosActualizados FROM tipoUsuario WHERE UserName = @UserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.id_usuario = int.Parse(dr["id_usuario"].ToString());
                        resultado.UserName = dr["UserName"].ToString();
                        resultado.DNI = dr["DNI"].ToString();
                        resultado.nombre = dr["nombre"].ToString();
                        resultado.apellido = dr["apellido"].ToString();
                        resultado.fecha_nac = DateTime.Parse(dr["fecha_nac"].ToString());
                        resultado.sexo = dr["sexo"].ToString();
                        resultado.telefono = dr["telefono"].ToString();
                        resultado.idProvincia =int.Parse(dr["idProvincia"].ToString());
                        resultado.ciudad = dr["ciudad"].ToString();
                        resultado.calle = dr["calle"].ToString();
                        resultado.altura = dr["altura"].ToString();
                        resultado.piso = dr["piso"].ToString(); 
                        resultado.datosActualizados = int.Parse(dr["datosActualizados"].ToString());

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

        public static bool ActualizarDatosUsuario(RegistroTipoUsuario r)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE tipoUsuario SET DNI = @DNI, nombre = @nombre, apellido = @apellido, fecha_nac = @fecha_nac , sexo = @sexo ,telefono = @telefono, idProvincia = @idProvincia , ciudad = @ciudad, calle = @calle, altura = @altura, piso = @piso, datosActualizados = 1 WHERE UserName = @UserName";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@DNI", r.DNI);
                cmd.Parameters.AddWithValue("@nombre", r.nombre);
                cmd.Parameters.AddWithValue("@apellido", r.apellido);
                cmd.Parameters.AddWithValue("@fecha_nac", r.fecha_nac);
                cmd.Parameters.AddWithValue("@sexo", r.sexo);
                cmd.Parameters.AddWithValue("@telefono", r.telefono);
                cmd.Parameters.AddWithValue("@idProvincia", r.idProvincia);
                cmd.Parameters.AddWithValue("@ciudad", r.ciudad);
                cmd.Parameters.AddWithValue("@calle", r.calle);
                cmd.Parameters.AddWithValue("@altura", r.altura);
                if (r.piso == null)
                {
                    cmd.Parameters.AddWithValue("@piso", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@piso", r.piso);
                }
               
                cmd.Parameters.AddWithValue("@UserName", r.UserName);

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


      
        //////////////////////////////////
        public static RegistroTipoEmpresa ObtenerEmpresa(string UserName)
        {
            RegistroTipoEmpresa resultado = new RegistroTipoEmpresa();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT idEmpresa, UserName, CUIT ,razonSocial ,nombreComercial ,idProvincia, ciudad, calle, altura, piso, paginaWeb " +
                    " , logo, comprobanteInsc FROM tipoEmpresa WHERE UserName = @UserName";

           
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        resultado.UserName = dr["UserName"].ToString();
                        resultado.CUIT = dr["CUIT"].ToString();
                        resultado.razonSocial = dr["razonSocial"].ToString();
                        resultado.nombreComercial = dr["nombreComercial"].ToString();
                        resultado.idProvincia = int.Parse(dr["idProvincia"].ToString());                    
                        resultado.ciudad = dr["ciudad"].ToString();
                        resultado.calle = dr["calle"].ToString();
                        resultado.altura = dr["altura"].ToString();
                        resultado.piso = dr["piso"].ToString();
                        resultado.paginaWeb = dr["paginaWeb"].ToString();
                        resultado.logo = (byte[])dr["logo"];                   
                        resultado.comprobanteInsc = (byte[])dr["comprobanteInsc"];

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


        public static RegistroTipoEmpresa ObtenerLogoEmpresa(string UserName)
        {
            RegistroTipoEmpresa resultado = new RegistroTipoEmpresa();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT idEmpresa, UserName, logo FROM tipoEmpresa WHERE UserName = @UserName";

              
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        resultado.UserName = dr["UserName"].ToString();                     
                        resultado.logo = (byte[])dr["logo"];

                        // resultado.comprobanteInsc = byte.Parse(dr["comprobanteInsc"].ToString());

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


        public static RegistroTipoEmpresa ObtenerComprobanteEmpresa(string UserName)
        {
            RegistroTipoEmpresa resultado = new RegistroTipoEmpresa();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT idEmpresa, UserName, comprobanteInsc FROM tipoEmpresa WHERE UserName = @UserName";

             
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserName", UserName);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {

                        resultado.idEmpresa = int.Parse(dr["idEmpresa"].ToString());
                        resultado.UserName = dr["UserName"].ToString();
                        resultado.comprobanteInsc = (byte[])dr["comprobanteInsc"];

                        // resultado.comprobanteInsc = byte.Parse(dr["comprobanteInsc"].ToString());

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

        

        public static bool ActualizarLogoEmpresa(RegistroTipoEmpresa r)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE tipoEmpresa SET logo = @logo WHERE UserName = @UserName";


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@logo", r.logo);
                cmd.Parameters.AddWithValue("@UserName", r.UserName);

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

        public static bool ActualizarComprobanteEmpresa(RegistroTipoEmpresa r)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE tipoEmpresa SET comprobanteInsc = @comprobanteInsc WHERE UserName = @UserName";


                cmd.Parameters.Clear();         
                cmd.Parameters.AddWithValue("@comprobanteInsc", r.comprobanteInsc);
                cmd.Parameters.AddWithValue("@UserName", r.UserName);

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


        public static bool ActualizarDatosEmpresa(RegistroTipoEmpresa r)
        {
            bool resultado = false;
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "UPDATE tipoEmpresa SET CUIT = @CUIT, razonSocial = @razonSocial, nombreComercial = @nombreComercial, " +
                    " idProvincia = @idProvincia , ciudad = @ciudad, calle = @calle, altura = @altura, piso = @piso, paginaWeb = @paginaWeb " +
                    " WHERE UserName = @UserName";


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@CUIT", r.CUIT);
                cmd.Parameters.AddWithValue("@razonSocial", r.razonSocial);
                cmd.Parameters.AddWithValue("@nombreComercial", r.nombreComercial);
                cmd.Parameters.AddWithValue("@idProvincia", r.idProvincia);
                cmd.Parameters.AddWithValue("@ciudad", r.ciudad);
                cmd.Parameters.AddWithValue("@calle", r.calle);
                cmd.Parameters.AddWithValue("@altura", r.altura);

                if (r.piso == null)
                {
                    cmd.Parameters.AddWithValue("@piso", "");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@piso", r.piso);
                }

             
                cmd.Parameters.AddWithValue("@paginaWeb", r.paginaWeb);
                cmd.Parameters.AddWithValue("@UserName", r.UserName);

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

