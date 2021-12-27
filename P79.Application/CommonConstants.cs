using System;
using System.Collections.Generic;

namespace P79.Application
{
    public static class CommonConstants
    {

        public static Dictionary<string, Type> ConverterMapings = new Dictionary<string, Type>
        {
            { "string", typeof(string) },
            { "int", typeof(int) },
            { "bool",typeof(bool) },
            { "guid", typeof(Guid) },
            { "date", typeof(DateTime) },
            //{ "UnitLevel",typeof(UnitLevel)},
            //{ "QuizStatus",typeof(QuizStatus)},
            //{ "NotificationType",typeof(NotificationType)},
            //{ "NotificationCategory",typeof(NotificationCategory)},
            //{ "ContentStatus",typeof(ContentStatus)},
            //{ "ContentType",typeof(ContentType)},
            //{ "ContentTreeType",typeof(ContentTreeType)}
        };
    }
}
