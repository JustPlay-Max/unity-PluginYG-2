using YG.Utils;

namespace YG
{
    public partial interface IPlatformsYG2
    {
        void SetString(string key, string value) => LocalStorage.SetKey(key, value);
        string GetString(string key) => LocalStorage.GetKey(key);
        string GetString(string key, string defaultValue) => LocalStorage.GetKey(key, defaultValue);

        void SetFloat(string key, float value) => LocalStorage.SetKey(key, value.ToString());
        float GetFloat(string key)
        {
            if (float.TryParse(LocalStorage.GetKey(key), out float res))
                return res;
            else
                return 0f;
        }
        float GetFloat(string key, float defaultValue)
        {
            if (!LocalStorage.HasKey(key))
                return defaultValue;
            else
                return float.Parse(LocalStorage.GetKey(key));
        }

        void SetInt(string key, int value) => LocalStorage.SetKey(key, value.ToString());
        int GetInt(string key)
        {
            if (int.TryParse(LocalStorage.GetKey(key), out int res))
                return res;
            else
                return 0;
        }
        int GetInt(string key, int defaultValue)
        {
            if (!LocalStorage.HasKey(key))
                return defaultValue;
            else
                return int.Parse(LocalStorage.GetKey(key));
        }

        bool HasKey(string key) => LocalStorage.HasKey(key);
        void DeleteKey(string key) => LocalStorage.DeleteKey(key);
        void DeleteAll() => LocalStorage.DeleteAll();
        void Save() { }
    }
}
