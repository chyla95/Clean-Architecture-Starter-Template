namespace Sample.Api.Common.Accessors;
public interface ISettingsAccessor
{
	string GetValue(string key);
	TValue GetValue<TValue>(string key);
}