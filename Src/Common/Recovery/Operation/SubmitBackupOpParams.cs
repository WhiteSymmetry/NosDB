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
using Alachisoft.NosDB.Common.Configuration.DOM;
using Alachisoft.NosDB.Common.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alachisoft.NosDB.Common.Recovery.Operation
{
    public class SubmitBackupOpParams : ICompactSerializable
    {
        private RecoveryPersistenceConfiguration _persistenceConfig;

        public SubmitBackupOpParams()
        {
            _persistenceConfig = new RecoveryPersistenceConfiguration();
        }


        public RecoveryPersistenceConfiguration PersistenceConfiguration
        {
            get { return _persistenceConfig; }
            set { _persistenceConfig = value; }
        }

        public void Deserialize(Serialization.IO.CompactReader reader)
        {
            PersistenceConfiguration = (RecoveryPersistenceConfiguration)reader.ReadObject();
        }

        public void Serialize(Serialization.IO.CompactWriter writer)
        {
            writer.WriteObject(PersistenceConfiguration);
        }
    }
}
