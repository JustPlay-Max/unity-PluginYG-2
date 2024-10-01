using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace YG.Example
{
    public class YandexMetricaExample : MonoBehaviour
    {
        public void TestSend1(string someEvent)
        {
            YG2.MetricaSend(someEvent);
        }

        public void TestSend1()
        {
            TestSend1("SomeEvent1");
        }

        public void TestSend2()
        {
            var eventParams2 = new Dictionary<string, string>
            {
                { "Complete", "1" },
                { "Money", "1500" }
            };

            YG2.MetricaSend("SomeEvent2", eventParams2);
        }

        public void TestSend3()
        {
            var eventParams3 = new Dictionary<string, string>
            {
                { "is_string", "RUB" },
                { "is_int", 1.ToString() },
                { "is_true", true.ToString() },
                { "is_false", false.ToString() }
            };

            YG2.MetricaSend("SomeEvent3", eventParams3);
        }

        public void TestSend4()
        {
            var eventParams3 = new Dictionary<string, string>
            {
                { "is_string", "RUB" },
                { "is_int", 1.ToString() },
                { "is_float", 2.5f.ToString(CultureInfo.InvariantCulture) },
                { "is_true", true.ToString() },
                { "is_false", false.ToString() },
                { "null_value", null },  // Проигнорируется и не будет отправленно 
                { string.Empty, null }   // Проигнорируется и не будет отправленно 
            };

            YG2.MetricaSend("SomeEvent4", eventParams3);
        }

        public void TestSend5()
        {
            var eventParams3 = new Dictionary<string, string>
            {
                { "null_value", null }
            };

            // Отправится как просто евент без параметров
            YG2.MetricaSend("SomeEvent5", eventParams3);
        }

        public void TestSend6_AddLevel(string someEvent, string param, string value)
        {
            var inParams = new Dictionary<string, string>
            {
                { someEvent, value }
            };
            var stringInParams = JsonUtility.ToJson(inParams);

            var eventParams = new Dictionary<string, string>
            {
                { param, stringInParams }
            };

            YG2.MetricaSend(someEvent, eventParams);
        }

        // Пример с вложением третьего уровня
        // Будто отправляем ивент указывая параметр вошёл игрок в тригер или вышел из него 
        public void TriggerEnteredSend(string nameTrigger, bool enter)
        {
            var inParams = new Dictionary<string, string>
                {
                    { name, enter.ToString() }
                };
            var stringInParams = JsonUtility.ToJson(inParams);

            var eventParams = new Dictionary<string, string>
                {
                    { "triggers", stringInParams }
                };

            YG2.MetricaSend("triggers", eventParams);
        }
    }
}