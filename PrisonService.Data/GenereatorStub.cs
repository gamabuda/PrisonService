using PrisonService.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonService.Data
{
    public static class GenereatorStub
    {
        public static List<Adress> Adresses { get; set; } = new List<Adress>()
        {
            new Adress("Набережные Челлны"),
            new Adress("Казань"),
            new Adress("Москва"),
            new Adress("Санкт-Питербург"),
            new Adress("Нижнекамск")
        };
        public static List<Prison> Prisons { get; set; } = new List<Prison>()
        {
            new Prison("Бутырка"),
            new Prison("Лефортово"),
            new Prison("Кресты"),
            new Prison("Чёрный дельфин"),
            new Prison("Белый лебедь")
        };
        public static List<Prisoner> Prisoners { get; set; } = new List<Prisoner>()
        {
            Prisoner.Create("Яппаров Дияр Рафаэльевич", Adresses.First(), new DateTime(2002, 12, 2), new Sex(true), "1233 344124", "Женат", "Неполное средне-специальное", null,
            "Особо опасен", "104", new DateTime(2034, 4, 12), Prisons.First(), true, "Облысенние, гипотит", true, "89494221222", false),
            Prisoner.Create("Карпов Данил Дмитриевич", Adresses.Last(), new DateTime(2003, 7, 27), new Sex(true), "4221 124124", "Не женат", "Средне-специальное", null,
            "Всыпльчевый, ходил на минтинги", "228", new DateTime(2030, 6, 11), Prisons.First(), false, "Задержка в развитии", false, "89249124113", true)
        };

        public static List<Employee> Employees { get; set; } = new List<Employee>()
        {
            Employee.Create("222111","Палков Михаил Андреевич", "Надзератель", "Черный дельфин", "123")
        };

    }
}
