using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    /*abstract*/class DBBase<T> where T : new()
    {
        public string Server { get; set; } = "localhost";
        public string User { get; set; } = "root";
        public string Pwd { get; set; } = "";
        public string Datenbank { get; set; } = "julian_CSharp";

        protected MySqlConnection connection = null;
        protected string tabelle;
        protected Dictionary<string, string> propsZuSpalten;

        public DBBase()
        {
            CreateConnection();
            MapSpaltenProperties();
            MapTabelle();
        }
        public DBBase(string s, string u, string p, string db) : this()
        {
            Server = s;
            User = u;
            Pwd = p;
            Datenbank = db;
        }
        public void CreateConnection()
        {
            connection = new MySqlConnection(CreateConnectionString());
        }
        public void Open()
        {
            connection.Open();
        }
        public void Close()
        {
            connection.Close();
        }
        public string CreateConnectionString()
        {
            //return $"server={server};user id=root; pwd=; database=julian_CSharp;allowuservariables=True; SslMode=none";
            return $"server={Server};user id={User}; pwd={Pwd}; database={Datenbank};allowuservariables=True; SslMode=none";
        }
        
        public List<T> AlleLesen()
        {
            List<T> elemente = new List<T>();
            string sql = SqlCommandAlleLesen();
            MySqlCommand command = new MySqlCommand(sql, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                T element = new T();
                Mapping(element, reader);
                elemente.Add(element);
            }
            return elemente;
        }

        private void Mapping(T element, MySqlDataReader reader)
        {
            // noch proggen
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (propsZuSpalten.ContainsKey(property.Name))
                {
                    if (property.PropertyType.IsEnum)
                    {
                        string stringValue = reader[propsZuSpalten[property.Name]].ToString();
                        property.SetValue(element, Enum.Parse(property.PropertyType, stringValue));
                        continue;
                    }
                    // braucht man eine Conversion?
                    //property.SetValue(element, reader[propsZuSpalten[property.Name]]);
                    property.SetValue(element, Convert.ChangeType(reader[propsZuSpalten[property.Name]], property.PropertyType));
                }
            }
        }
        public string SqlCommandAlleLesen()
        {
            return $"SELECT * FROM {tabelle}";
        }

        private void MapSpaltenProperties()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    SpalteAttribute spaltenAttribut = attr as SpalteAttribute;
                    if (spaltenAttribut != null)
                    {
                        string propName = prop.Name;
                        string spaltenName = spaltenAttribut.SpaltenName;

                        dict.Add(propName, spaltenName);
                    }
                }
            }

            propsZuSpalten = dict;
        }
        private void MapTabelle()
        {
            object[] attrs = typeof(T).GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                TabelleAttribute tabellenAttribut = attr as TabelleAttribute;
                if (tabellenAttribut != null)
                {
                    tabelle = tabellenAttribut.TabellenName;
                    break;
                }
            }
        }
    }
}
