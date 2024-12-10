#if YandexGamesPlatform_yg
namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuild
    {
        public static void Metrica()
        {
            if (infoYG.Metrica.enable)
            {
                string copyCode = FileTextCopy("Metrica_html.html");
                copyCode = copyCode.Replace("___METRICA_COUNTER_ID___", infoYG.Metrica.metricaCounterID.ToString());

                AddIndexCode(copyCode, CodeType.Head);
            }
        }
    }
}
#endif