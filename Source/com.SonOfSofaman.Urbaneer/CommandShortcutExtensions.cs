using System;
using System.ComponentModel;

namespace com.SonOfSofaman.Urbaneer
{
	public static class CommandShortcutExtensions
	{
		public static T GetEnumFromCommandShortcut<T>(this string shortcut)
		{
			var type = typeof(T);
			if (!type.IsEnum) throw new InvalidOperationException();

			foreach (var field in type.GetFields())
			{
				CommandShortcutAttribute attribute = Attribute.GetCustomAttribute(field, typeof(CommandShortcutAttribute)) as CommandShortcutAttribute;
				if (attribute == null)
				{
					if (field.Name == shortcut) return (T)field.GetValue(null);
				}
				else
				{
					if (attribute.Shortcut == shortcut) return (T)field.GetValue(null);
				}
			}
			throw new ArgumentException("Not found.", "shortcut");
		}
	}
}
