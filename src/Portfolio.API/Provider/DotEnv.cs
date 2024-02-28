namespace Portfolio.API.Provider;

public static class DotEnv
{
	public static void Load()
	{
		var root = Directory.GetParent("../../").ToString();

		var dotEnv = Path.Combine(root, ".env");

		if (!File.Exists(dotEnv))
			return;

		foreach (var line in File.ReadAllLines(dotEnv))
		{
			var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

			if (parts.Length != 2)
				continue;

			Environment.SetEnvironmentVariable(parts[0], parts[1]);
		}
	}
}
