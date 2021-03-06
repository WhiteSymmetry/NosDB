﻿// /*
// * Copyright (c) 2016, Alachisoft. All Rights Reserved.
// *
// * Licensed under the Apache License, Version 2.0 (the "License");
// * you may not use this file except in compliance with the License.
// * You may obtain a copy of the License at
// *
// * http://www.apache.org/licenses/LICENSE-2.0
// *
// * Unless required by applicable law or agreed to in writing, software
// * distributed under the License is distributed on an "AS IS" BASIS,
// * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// * See the License for the specific language governing permissions and
// * limitations under the License.
// */
using System;
using Alachisoft.NosDB.Common.Serialization;
using Alachisoft.NosDB.Common.Server.Engine;
using Newtonsoft.Json;

namespace Alachisoft.NosDB.Common.Configuration.Services
{

    public class PartitionKey : ICloneable,  ICompactSerializable
    {
        /// <summary>
        /// Gets/Sets the names of the attributes participating in partition key
        /// </summary>
        [JsonProperty(PropertyName = "Attributes")]
        public PartitionKeyAttribute[] Attributes { get; set; }

        /*[JsonProperty(PropertyName = "AutoGenerated")]
        public bool AutoGenerated { get; set; }*/

        /*public DocumentKey GetFormattedPartitionKey(params string[] partitionKeyValues)
        {
            string key = null;
            if (AutoGenerated)
            {
                key = GenerateKey();
            }
            else
            {
                StringBuilder sb = new StringBuilder();

                foreach (string partitionKeyValue in partitionKeyValues)
                {
                    if (sb.Length > 0)
                        sb.Append(":");
                    sb.Append(partitionKeyValue);
                }

                key = sb.ToString();
            }

            return new DocumentKey(key);
        }*/

        /// <summary>
        /// Get the value for each attribute in the Document. Concat them to make a key out of it.
        /// If even one of the attribute does not exists inside the document, it returns null
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public string GetStringFormattedPartitionKey(IJSONDocument document)
        {
            string key = string.Empty;
            foreach (var partitionKeyAttribute in Attributes)
            {
                if (document.Contains(partitionKeyAttribute.Name))
                {
                    key = string.Concat(key, document.GetToString(partitionKeyAttribute.Name));
                }
                else
                {
                    return null;
                }
            }
            return key.Equals(string.Empty) ? null : key;
        }

        private string GenerateKey()
        {
            return Guid.NewGuid().ToString();
        }

        #region ICloneable Member
        public object Clone()
        {
            PartitionKey partitionKey = new PartitionKey();
            partitionKey.Attributes = Attributes != null ? (PartitionKeyAttribute[])Attributes.Clone() : null;
            //partitionKey.AutoGenerated = AutoGenerated;

            return partitionKey;
        } 
        #endregion

        #region ICompactSerializable Members
        public void Deserialize(Common.Serialization.IO.CompactReader reader)
        {
            Attributes = reader.ReadObject() as PartitionKeyAttribute[];
            //AutoGenerated = reader.ReadBoolean();
        }

        public void Serialize(Common.Serialization.IO.CompactWriter writer)
        {
            writer.WriteObject(Attributes);
            //writer.Write(AutoGenerated);
        } 
        #endregion
    }
}
