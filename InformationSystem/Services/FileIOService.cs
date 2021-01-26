using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;

namespace InformationSystem
{
    /// <summary>
    /// Класс для работы с хранилищем на компьютере.
    /// </summary>
    internal class FileIOService
    {
        public MainWindow _w;

        public FileIOService(MainWindow w) => _w = w;

        public void SaveData(Group organization)
        {
            // Сериализуем данные.
            string json = JsonConvert.SerializeObject(organization, Formatting.Indented);
            
            SaveFileDialog window = new SaveFileDialog();
            window.InitialDirectory = Directory.GetCurrentDirectory();
            window.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";
            window.FileName = "organization";

            // Сохраняем.
            if (window.ShowDialog() == true)
                File.WriteAllText(window.FileName, json);
        }

        public Group UnloadData()
        {
            OpenFileDialog window = new OpenFileDialog();
            window.InitialDirectory = Directory.GetCurrentDirectory();
            window.Filter = "JSON file (*.json)|*.json|All files (*.*)|*.*";
            window.FileName = "organization";

            if (window.ShowDialog() == true)
            {
                try
                {
                    string json = File.ReadAllText(window.FileName);
                    JsonConverter[] converters = { new EmployeeConverter() };
                    return JsonConvert.DeserializeObject<Group>(json, new JsonSerializerSettings() { Converters = converters });
                }
                catch
                {
                    MessageBox.Show("File reading error!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }
            } else return null;
        }
    }

    internal class EmployeeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Employee));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            if (jo["Position"].Value<string>() == "CEO")
                return jo.ToObject<CEO>(serializer);

            if (jo["Position"].Value<string>() == "Administrator")
                return jo.ToObject<Administrator>(serializer);

            if (jo["Position"].Value<string>() == "Manager")
                return jo.ToObject<Manager>(serializer);

            if (jo["Position"].Value<string>() == "Staff")
                return jo.ToObject<Staff>(serializer);

            if (jo["Position"].Value<string>() == "Intern")
                return jo.ToObject<Intern>(serializer);

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
