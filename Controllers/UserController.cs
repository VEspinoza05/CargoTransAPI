using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Firestore;
using CargoTransAPI.Models;

namespace CargoTransAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;

        public UserController(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = new List<UserModel>();

            try
            {
                var usersCollection = _firestoreDb.Collection("user");

                if (usersCollection != null)
                {
                    var userList = await usersCollection.GetSnapshotAsync();

                    foreach (var user in userList.Documents)
                    {
                        // create user object using the fields of the record
                        UserModel u = new UserModel();
                        u.Name = user.GetValue<string>("name");
                        u.Email = user.GetValue<string>("email");

                        users.Add(u);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }
            return Ok(users);
        }
    }
}