// ------------------------------------------------------------------------------
//      This code was generated by a tool.
// 
//      Changes to this file will be lost if the code is regenerated.
// ------------------------------------------------------------------------------

using System;
using Vici.CoolStorage;

namespace BridgeTry.Backend.Core.Model
{
	namespace Entities
	{
		[MapTo("TestData")]
		public partial class TestData : CSObject<TestData, int>
		{
			
			[MapTo("ID"), Identity]
			public int ID {
				get { return (int)GetField("ID"); }
			}

			[MapTo("TestText"), NullValue("")]
			public string TestText {
				get { return (string)GetField("TestText"); }
				set { SetField("TestText", value); }
			}

			[MapTo("TestBlob")]
			public byte[] TestBlob {
				get { return (byte[])GetField ("TestBlob"); }
				set { SetField ("TestBlob", value); }
			}
			
			[MapTo("TestDouble"), NullValue(0.0)]
			public double TestDouble {
				get { return (double)GetField("TestDouble"); }
				set { SetField("TestDouble", value); }
			}


			//filling all fields of new objects speeds up reading later on significantly
			public TestData PopulateDefaultValues ()
			{
				this.TestText = "";
				this.TestBlob = null;
				this.TestDouble = 0.0;

				return this;
			}
			
		}
	}
}