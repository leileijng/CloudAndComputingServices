using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using TLTTSaaSWebApp.Filters;
using TLTTSaaSWebApp.Models;
using MySql.Data.MySqlClient;

namespace TLTTSaaSWebApp.APIs
{
    [Authorize]
    public class TalentsController : ApiController
    {
        static readonly TalentRepository repository = new TalentRepository();

        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = "Talents";


            string username = "admin";
            string password = "cscassignment2";
            string hostname = "cscassignment2.cttfbuunfxjz.us-east-1.rds.amazonaws.com";
            string port = "3306";

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
        
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/talents")]
        [AllowAnonymous]
        public List<Talent> GetAllTalents()
        {
            List<Talent> talentsToReturn = new List<Talent>();
            MySql.Data.MySqlClient.MySqlConnection conn;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = GetRDSConnectionString();
                conn.Open();

                string query = "SELECT * FROM talent";
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Talent t = new Talent();
                    Debug.WriteLine(reader["shortname"].ToString());
                    t.Id = int.Parse(reader["id"].ToString());
                    t.Name = reader["name"].ToString();
                    t.ShortName = reader["shortname"].ToString();
                    t.Bio = reader["bio"].ToString();
                    t.Photo = reader["photo"].ToString();
                    t.Reknown = reader["reknown"].ToString();
                    talentsToReturn.Add(t);
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return talentsToReturn;
        }


        [HttpGet]
        [Route("api/talents/{id:int}", Name = "getTalentById")]
        [AllowAnonymous]
        public Talent GetTalent(int id)
        {
            Talent t = new Talent();
            
            MySql.Data.MySqlClient.MySqlConnection conn;
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = GetRDSConnectionString();
                conn.Open();

                string query = "SELECT * FROM talent WHERE id = '" + id + "';";
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Debug.WriteLine(reader["shortname"].ToString());
                    t.Id = id;
                    t.Name = reader["name"].ToString();
                    t.ShortName = reader["shortname"].ToString();
                    t.Bio = reader["bio"].ToString();
                    t.Photo = reader["photo"].ToString();
                    t.Reknown = reader["reknown"].ToString();
                }
                conn.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return t;
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        [Route("api/talents")]
        [AllowAnonymous]
        public IHttpActionResult PostTalent(Talent item)
        {
            if (!(String.IsNullOrWhiteSpace(item.Name) || String.IsNullOrWhiteSpace(item.ShortName) ||
                    String.IsNullOrWhiteSpace(item.Bio) || String.IsNullOrWhiteSpace(item.Reknown)) &&
                    ModelState.IsValid)
            {
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection conn;
                    string myConnectionString;
                    myConnectionString = GetRDSConnectionString();

                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    string query = "INSERT INTO `Talents`.`talent` (`name`, `shortname`, `photo`, `reknown`, `bio`) VALUES('" + MySqlHelper.EscapeString(item.Name) + "', '" + MySqlHelper.EscapeString(item.ShortName) + "', '" + MySqlHelper.EscapeString(item.Photo) + "', '" + MySqlHelper.EscapeString(item.Reknown) + "', '" + MySqlHelper.EscapeString(item.Bio) + "')";
                    Debug.WriteLine(query);
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Ok();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPut]
        [Route("api/talents/{id:int}")]
        [AllowAnonymous]
        public IHttpActionResult PutTalent(int id, Talent item)
        {
            if (!(String.IsNullOrWhiteSpace(item.Name) || String.IsNullOrWhiteSpace(item.ShortName) ||
                     String.IsNullOrWhiteSpace(item.Bio) || String.IsNullOrWhiteSpace(item.Reknown)) &&
                     ModelState.IsValid)
            {
                try
                {
                    MySql.Data.MySqlClient.MySqlConnection conn;
                    string myConnectionString;
                    myConnectionString = GetRDSConnectionString();

                    conn = new MySql.Data.MySqlClient.MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    conn.Open();
                    string query = "UPDATE `Talents`.`talent` SET `name`='" + MySqlHelper.EscapeString(item.Name)+"', `shortname`='" + MySqlHelper.EscapeString(item.ShortName)+"', `photo`='"+ MySqlHelper.EscapeString(item.Photo)+"', `reknown`='" + MySqlHelper.EscapeString(item.Reknown)+"', `bio`='" + MySqlHelper.EscapeString(item.Bio)+"' WHERE `id`='"+id+"';";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message.ToString());
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpDelete]
        [AllowAnonymous]
        [Route("api/talents/{id:int}")]
        public IHttpActionResult DeleteTalent(int id)
        {
            try
            {
                MySql.Data.MySqlClient.MySqlConnection conn;
                string myConnectionString;
                myConnectionString = GetRDSConnectionString();

                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                string query = "DELETE FROM `Talents`.`talent` WHERE `id`='"+id+"';";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message.ToString());
                Debug.WriteLine("Delete error");
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}