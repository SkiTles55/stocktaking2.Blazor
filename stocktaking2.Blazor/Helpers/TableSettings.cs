using System.Collections.Generic;
using System.Text.Json;

namespace stocktaking2.Blazor.Helpers
{
    public class TableSettings
    {
        public static Dictionary<string, string> Collumns = new Dictionary<string, string>
        {
            ["status"] = "Статус",
            ["category"] = "Тип",
            ["man"] = "Производитель",
            ["model"] = "Модель",
            ["location"] = "Местоположение",
            ["invent"] = "Инвентарный номер",
            ["serial"] = "Серийный номер",
            ["buydate"] = "Дата приобретения",
            ["installdate"] = "Дата установки",
            ["employer"] = "Сотрудник",
            ["departament"] = "Отдел",
            ["winname"] = "Операционная система",
            ["processor"] = "Процессор",
            ["motherboard"] = "Материнская плата",
            ["DDR"] = "Оперативная память",
            ["specs"] = "Характеристики",
            ["cartridgemodel"] = "Модель картриджа",
            ["cartridgecount"] = "Количество картриджей",
            ["ipadresses"] = "IP адреса",
            ["netname"] = "Сетевое имя",
            ["biospass"] = "Пароль BIOS"
        };

        public static string GetDefaultSettings()
        {
            List<string> Default = new List<string>()
            {
                "status", "category", "man", "model", "location", "invent", "installdate", "employer", "departament"
            };

            return JsonSerializer.Serialize(Default);
        }

        public static List<string> DeserializeSettings(string json) => JsonSerializer.Deserialize<List<string>>(json);

        public static string SerializeSettings(List<string> items) => JsonSerializer.Serialize(items);
    }
}