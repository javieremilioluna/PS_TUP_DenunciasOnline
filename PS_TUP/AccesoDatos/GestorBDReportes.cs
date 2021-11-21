using PS_TUP.Models.DTO;
using PS_TUP.Models.Reportes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PS_TUP.AccesoDatos
{
    public class GestorBDReportes
    {

        public static List<DenunciasPorMes> DenunciasPorMes()
        {
            List<DenunciasPorMes> resultado = new List<DenunciasPorMes>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT COUNT(IdDenuncia) cantidad,M.idMes FROM Denuncias D RIGHT JOIN " +
                    " MESES M ON MONTH(D.Fecha) = M.idMes GROUP BY M.idMes ORDER BY M.idMes";

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
                        DenunciasPorMes d = new DenunciasPorMes();
                        d.cantidad = int.Parse(dr["cantidad"].ToString());
                        d.mes = int.Parse(dr["idMes"].ToString());
               
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


        public static List<DenunciasPorAnio> DenunciasPorAnioPorTipo(string IdTipoDenuncia)
        {
            List<DenunciasPorAnio> resultado = new List<DenunciasPorAnio>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT COUNT(d.IdTipoDenuncia) cantidad, m.DescripcionMotivo, YEAR(d.Fecha) anio FROM Denuncias d, " +
                    " MotivosDenuncias m WHERE m.DescripcionMotivo = d.IdTipoDenuncia AND m.DescripcionMotivo = @IdTipoDenuncia " +
                    " GROUP BY m.DescripcionMotivo, YEAR(d.Fecha) ";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdTipoDenuncia", IdTipoDenuncia);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        DenunciasPorAnio d = new DenunciasPorAnio();
                        d.cantidad = int.Parse(dr["cantidad"].ToString());
                        d.anio = int.Parse(dr["anio"].ToString());

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




        public static List<CantDenunciasPorTipo> CantDenunciasPorTipos()
        {
            List<CantDenunciasPorTipo> resultado = new List<CantDenunciasPorTipo>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT  COUNT(IdDenuncia) cantidad, m.IdTipoDenuncia FROM Denuncias D RIGHT join " +
                    " MotivosDenuncias m ON d.IdTipoDenuncia = M.DescripcionMotivo GROUP BY m.IdTipoDenuncia ORDER BY m.IdTipoDenuncia";

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
                        CantDenunciasPorTipo d = new CantDenunciasPorTipo();
                        d.cantidad = int.Parse(dr["cantidad"].ToString());
                        d.IdTipoDenuncia = int.Parse(dr["IdTipoDenuncia"].ToString());

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


        public static List<FiltroRangoFechas> FiltroCantDenunciasPorTipos(DateTime desde, DateTime hasta)
        {
            List<FiltroRangoFechas> resultado = new List<FiltroRangoFechas>();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT COUNT(IdDenuncia) cantidad, m.IdTipoDenuncia FROM Denuncias D RIGHT join MotivosDenuncias m " +
                    " ON(d.IdTipoDenuncia = M.DescripcionMotivo AND d.Fecha BETWEEN @desde AND @hasta) GROUP BY m.IdTipoDenuncia " +
                    " ORDER BY m.IdTipoDenuncia ";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = consulta;

                conexion.Open();
                cmd.Connection = conexion;

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        FiltroRangoFechas d = new FiltroRangoFechas();
                        d.cantidad = int.Parse(dr["cantidad"].ToString());
                        d.IdTipoDenuncia = int.Parse(dr["IdTipoDenuncia"].ToString());
                    
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


        /////////////////////////////////////////////////////////////////
        public static CantidadEmprVerificadas EmprVerificadas()
        {
            CantidadEmprVerificadas resultado = new CantidadEmprVerificadas();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT CAST((CONVERT(FLOAT,(COUNT(*)*100))/(select count(*) FROM tipoEmpresa WHERE verificada <> 'Otro') ) " +
                    " as numeric(36,2))  AS porcentaje FROM tipoEmpresa WHERE verificada = 'Si' ";

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

                        resultado.porcentaje = double.Parse(dr["porcentaje"].ToString());
                     
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


        public static CantidadEmprVerificadas EmprNoVerificadas()
        {
            CantidadEmprVerificadas resultado = new CantidadEmprVerificadas();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT CAST((CONVERT(FLOAT,(COUNT(*)*100))/(select count(*) FROM tipoEmpresa WHERE verificada <> 'Otro') ) " +
                    " as numeric(36,2))  AS porcentaje FROM tipoEmpresa WHERE verificada = 'No' ";

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

                        resultado.porcentaje = double.Parse(dr["porcentaje"].ToString());

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


        public static CantRegPorTipo CantidadEmpresasRegistradas()
        {
            CantRegPorTipo resultado = new CantRegPorTipo();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT COUNT(*) Cantidad, Role Rol FROM Usuarios u, tipoEmpresa t WHERE " +
                                  " Role = 'Empresa' AND u.UserName = t.UserName AND t.verificada <> 'Otro' GROUP BY Role ";

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

                        resultado.cantidad = int.Parse(dr["Cantidad"].ToString());

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

        public static CantRegPorTipo CantidadUsuariosRegistradas()
        {
            CantRegPorTipo resultado = new CantRegPorTipo();
            SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CadenaBD"].ConnectionString);

            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = " SELECT COUNT(*) Cantidad, Role Rol FROM Usuarios u, tipoUsuario t " +
                                  " WHERE Role = 'Usuario' AND u.UserName = t.UserName GROUP BY Role ";

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

                        resultado.cantidad = int.Parse(dr["Cantidad"].ToString());

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
    }
}