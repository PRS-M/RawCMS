﻿using Newtonsoft.Json.Linq;
using RawCMS.Library.Core;
using RawCMS.Library.Core.Interfaces;
using RawCMS.Library.Service;
using RawCMS.Plugins.Core.Stores;
using System.Collections.Generic;

namespace RawCMS.Plugins.Core.Data
{
    public class UserPresaveLambda : PreSaveLambda, IRequireCrudService
    {
        public override string Name => "User Presave lambda";

        public override string Description => "provide normalized name and prevent password change";

        private CRUDService service;

        public override void Execute(string collection, ref JObject item, ref Dictionary<string, object> dataContext)
        {
            if (collection == "_users")
            {
                if (item.ContainsKey("UserName"))
                {
                    item["NormalizedUserName"] = RawUserStore.NormalizeString(item["UserName"].Value<string>());
                }
                if (item.ContainsKey("NormalizedEmail"))
                {
                    item["NormalizedEmail"] = RawUserStore.NormalizeString(item["NormalizedEmail"].Value<string>());
                }
                if (item.ContainsKey("NewPassword"))
                {
                    dataContext["NewPassword"] = item["NewPassword"].Value<string>();
                    item.Remove("NewPassword");
                }
                if (item.ContainsKey("PasswordHash"))
                {
                    item.Remove("PasswordHash");
                }
            }
        }

        public void SetCRUDService(CRUDService service)
        {
            this.service = service;
        }
    }
}