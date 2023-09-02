// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY MPC(MessagePack-CSharp). DO NOT CHANGE IT.
// </auto-generated>

#pragma warning disable 618
#pragma warning disable 612
#pragma warning disable 414
#pragma warning disable 168
#pragma warning disable CS1591 // document public APIs

#pragma warning disable SA1312 // Variable names should begin with lower-case letter
#pragma warning disable SA1649 // File name should match first type name

namespace MessagePack.Resolvers
{
    public class GeneratedResolver : global::MessagePack.IFormatterResolver
    {
        public static readonly global::MessagePack.IFormatterResolver Instance = new GeneratedResolver();

        private GeneratedResolver()
        {
        }

        public global::MessagePack.Formatters.IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.Formatter;
        }

        private static class FormatterCache<T>
        {
            internal static readonly global::MessagePack.Formatters.IMessagePackFormatter<T> Formatter;

            static FormatterCache()
            {
                var f = GeneratedResolverGetFormatterHelper.GetFormatter(typeof(T));
                if (f != null)
                {
                    Formatter = (global::MessagePack.Formatters.IMessagePackFormatter<T>)f;
                }
            }
        }
    }

    internal static class GeneratedResolverGetFormatterHelper
    {
        private static readonly global::System.Collections.Generic.Dictionary<global::System.Type, int> lookup;

        static GeneratedResolverGetFormatterHelper()
        {
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(9)
            {
                { typeof(global::Devarc.ErrorType), 0 },
                { typeof(global::Devarc.GenderType), 1 },
                { typeof(global::Auth2C.NotifyLogin), 2 },
                { typeof(global::C2Auth.RequestLogin), 3 },
                { typeof(global::C2Game.RequestLogin), 4 },
                { typeof(global::Devarc.Account), 5 },
                { typeof(global::Devarc.CHARACTER), 6 },
                { typeof(global::Devarc.SKILL), 7 },
                { typeof(global::Game2C.NotifyLogin), 8 },
            };
        }

        internal static object GetFormatter(global::System.Type t)
        {
            int key;
            if (!lookup.TryGetValue(t, out key))
            {
                return null;
            }

            switch (key)
            {
                case 0: return new MessagePack.Formatters.Devarc.ErrorTypeFormatter();
                case 1: return new MessagePack.Formatters.Devarc.GenderTypeFormatter();
                case 2: return new MessagePack.Formatters.Auth2C.NotifyLoginFormatter();
                case 3: return new MessagePack.Formatters.C2Auth.RequestLoginFormatter();
                case 4: return new MessagePack.Formatters.C2Game.RequestLoginFormatter();
                case 5: return new MessagePack.Formatters.Devarc.AccountFormatter();
                case 6: return new MessagePack.Formatters.Devarc.CHARACTERFormatter();
                case 7: return new MessagePack.Formatters.Devarc.SKILLFormatter();
                case 8: return new MessagePack.Formatters.Game2C.NotifyLoginFormatter();
                default: return null;
            }
        }
    }
}

#pragma warning restore 168
#pragma warning restore 414
#pragma warning restore 618
#pragma warning restore 612

#pragma warning restore SA1312 // Variable names should begin with lower-case letter
#pragma warning restore SA1649 // File name should match first type name
