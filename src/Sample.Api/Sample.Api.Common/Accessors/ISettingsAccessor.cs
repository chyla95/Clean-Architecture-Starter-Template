namespace MC.Sample.Api.Utilities.Accessors;

public interface ISettingsAccessor
{
	string GetValue(string key);
	TValue GetValue<TValue>(string key);
}