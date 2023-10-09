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
            lookup = new global::System.Collections.Generic.Dictionary<global::System.Type, int>(21)
            {
                { typeof(global::Devarc.ErrorType), 0 },
                { typeof(global::Devarc.GenderType), 1 },
                { typeof(global::Auth2C.NotifyError), 2 },
                { typeof(global::Auth2C.NotifyLogin), 3 },
                { typeof(global::Auth2C.NotifyLogout), 4 },
                { typeof(global::Auth2C.NotifySession), 5 },
                { typeof(global::Auth2C.NotifySignin), 6 },
                { typeof(global::C2Auth.RequestLogin), 7 },
                { typeof(global::C2Auth.RequestLogout), 8 },
                { typeof(global::C2Auth.RequestSession), 9 },
                { typeof(global::C2Auth.RequestSignin), 10 },
                { typeof(global::C2Game.RequestLogin), 11 },
                { typeof(global::Devarc.Account), 12 },
                { typeof(global::Devarc.CHARACTER), 13 },
                { typeof(global::Devarc.CommonResult), 14 },
                { typeof(global::Devarc.CustomSigninResult), 15 },
                { typeof(global::Devarc.GoogleCodeResult), 16 },
                { typeof(global::Devarc.GoogleSigninResult), 17 },
                { typeof(global::Devarc.SKILL), 18 },
                { typeof(global::Devarc.SOUND), 19 },
                { typeof(global::Game2C.NotifyLogin), 20 },
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
                case 2: return new MessagePack.Formatters.Auth2C.NotifyErrorFormatter();
                case 3: return new MessagePack.Formatters.Auth2C.NotifyLoginFormatter();
                case 4: return new MessagePack.Formatters.Auth2C.NotifyLogoutFormatter();
                case 5: return new MessagePack.Formatters.Auth2C.NotifySessionFormatter();
                case 6: return new MessagePack.Formatters.Auth2C.NotifySigninFormatter();
                case 7: return new MessagePack.Formatters.C2Auth.RequestLoginFormatter();
                case 8: return new MessagePack.Formatters.C2Auth.RequestLogoutFormatter();
                case 9: return new MessagePack.Formatters.C2Auth.RequestSessionFormatter();
                case 10: return new MessagePack.Formatters.C2Auth.RequestSigninFormatter();
                case 11: return new MessagePack.Formatters.C2Game.RequestLoginFormatter();
                case 12: return new MessagePack.Formatters.Devarc.AccountFormatter();
                case 13: return new MessagePack.Formatters.Devarc.CHARACTERFormatter();
                case 14: return new MessagePack.Formatters.Devarc.CommonResultFormatter();
                case 15: return new MessagePack.Formatters.Devarc.CustomSigninResultFormatter();
                case 16: return new MessagePack.Formatters.Devarc.GoogleCodeResultFormatter();
                case 17: return new MessagePack.Formatters.Devarc.GoogleSigninResultFormatter();
                case 18: return new MessagePack.Formatters.Devarc.SKILLFormatter();
                case 19: return new MessagePack.Formatters.Devarc.SOUNDFormatter();
                case 20: return new MessagePack.Formatters.Game2C.NotifyLoginFormatter();
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
