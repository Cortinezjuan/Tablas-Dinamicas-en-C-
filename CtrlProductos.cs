﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TablasDinamicas
{
    class CtrlProductos : Conexion
    {
        public List<Object> consulta(String dato)
        {
            MySqlDataReader reader;
            List<Object> lista = new List<object>();
            string sql;
            if(dato == null)
            {
                sql = "SELECT codigo, nombre, descripcion, precio_publico, existencias FROM datosproductos ORDER BY nombre ASC";

            }
            else
            {
                sql = "SELECT codigo, nombre, descripcion, precio_publico, existencias FROM datosproductos WHERE codigo LIKE'%"+dato+ "%' OR nombre LIKE'%" + dato + "%' OR precio_publico LIKE'%" + dato + "%' OR existencias LIKE'%" + dato + "%' ORDER BY nombre ASC";
            }
            try
            {
                MySqlConnection conexionBD = base.conexion();
                conexionBD.Open();
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Productos _producto = new Productos();
                    _producto.Codigo = int.Parse(reader.GetString(0));
                    _producto.Nombre = reader.GetString("nombre");
                    _producto.Descripcion = reader[2].ToString();
                    _producto.Precio_publico = double.Parse(reader[3].ToString());
                    _producto.Existencias = int.Parse(reader.GetString(4));
                    lista.Add(_producto);

                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return lista;
            
        }
    }
}
