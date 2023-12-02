using Microsoft.Extensions.Configuration;

namespace Sample.Api.Common.Accessors;

public class SettingsAccessor : ISettingsAccessor
{
	private readonly IConfiguration _configuration;

	public SettingsAccessor(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string GetValue(string key)
	{
		string value = GetValue(key, _configuration);
		return value;
	}

	public TValue GetValue<TValue>(string key)
	{
		TValue value = GetValue<TValue>(key, _configuration);
		return value;
	}

	public static string GetValue(string key, IConfiguration configuration)
	{
		return GetValue<string>(key, configuration);
	}

	public static TValue GetValue<TValue>(string key, IConfiguration configuration)
	{
		if (string.IsNullOrWhiteSpace(key)) throw new NullReferenceException(nameof(key));

		TValue? value = configuration.GetSection(key).Get<TValue>();
		if (value is null) throw new NullReferenceException(nameof(value));

		return value;
	}
}
