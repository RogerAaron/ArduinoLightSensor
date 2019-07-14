using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Serial.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{ 
		// GET api/values/5
		[HttpGet]
		public string Get(string id)
		{
			SendToDB(id);
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]TestData testData)
		{
			SendToDB(testData);
		}

		private void SendToDB(TestData dataValue)
		{
			if (!string.IsNullOrEmpty(dataValue.DataValue) && dataValue.DataValue != "0")
			{
				SqlConnection myCon = null;
				myCon = new SqlConnection("Data Source=db204.my-hosting-panel.com;Initial Catalog=sym147_arduinodb;Connection Timeout=180;Persist Security Info=True;User ID=sym147_projectarduino;Password=arduinosam147");
				try
				{

					myCon.Open();

					SqlCommand cmd = new SqlCommand("spAddEdit", myCon);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("DataValue", dataValue.DataValue);
					cmd.Parameters.AddWithValue("CurrentDateTime", DateTime.Now);
					cmd.CommandTimeout = 300;
					cmd.ExecuteNonQuery();

					myCon.Close();
				}
				catch (Exception ex)
				{
					string exception = ex.Message;
				}
				finally
				{
					myCon.Close();
				}
			}
		}

		private void SendToDB(string id)
		{
			if (!string.IsNullOrEmpty(id) && id != "0")
			{
				SqlConnection myCon = null;
				myCon = new SqlConnection("Data Source=db204.my-hosting-panel.com;Initial Catalog=sym147_arduinodb;Connection Timeout=180;Persist Security Info=True;User ID=sym147_projectarduino;Password=arduinosam147");
				try
				{
					myCon.Open();

					SqlCommand cmd = new SqlCommand("spAddEdit", myCon);
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.AddWithValue("DataValue", id);
					cmd.Parameters.AddWithValue("CurrentDateTime", DateTime.Now);
					cmd.CommandTimeout = 300;
					cmd.ExecuteNonQuery();

					myCon.Close();
				}
				catch (Exception ex)
				{
					string exception = ex.Message;
				}
				finally
				{
					myCon.Close();
				}
			}
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}

	public class TestData
	{
		public string DataValue { get; set; }
		
	}

}
