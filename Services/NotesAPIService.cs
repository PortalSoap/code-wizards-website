using Newtonsoft.Json;
using System.Text;
using code_wizards_website.Models;

namespace code_wizards_website.Services
{
    public static class NotesAPIService
    {
        private static HttpClient _httpClient = new HttpClient();
        
        public static async Task<List<UserModel>> GetAllUsersAsync()
        {
            using(HttpResponseMessage response = await _httpClient.GetAsync("https://notes-cw.azurewebsites.net/NotesAPI/User"))
            {
                if(response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<UserModel> users = JsonConvert.DeserializeObject<List<UserModel>>(jsonResponse);
                    return users;
                }

                else
                {
                    throw new HttpRequestException($"Requisiton Error: A requisição foi mal-sucedida ({response.StatusCode})");
                }
            }
        }

        public static async Task<List<NoteModel>> NotesFromEmailAsync(string email)
        {
            using(HttpResponseMessage response = await _httpClient.GetAsync("https://notes-cw.azurewebsites.net/NotesAPI/Note"))
            {
                if(response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<NoteModel> allNotes = JsonConvert.DeserializeObject<List<NoteModel>>(jsonResponse);
                    List<NoteModel> filteredList = allNotes.FindAll(x => x.User?.Email == email);
                    return filteredList;
                }

                else
                {
                    throw new HttpRequestException($"Requisiton Error: A requisição foi mal-sucedida ({response.StatusCode})");
                }
            }
        }

        public static async Task<NoteModel> NoteByIdAsync(int id)
        {
            using (HttpResponseMessage response = await _httpClient.GetAsync($"https://notes-cw.azurewebsites.net/NotesAPI/Note/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    NoteModel target = JsonConvert.DeserializeObject<NoteModel>(jsonResponse);

                    return target;
                }

                else
                {
                    throw new HttpRequestException($"Requisiton Error: A requisição foi mal-sucedida ({response.StatusCode})");
                }
            }
        }

        public static async Task AddUserAsync(UserModel user)
        {
            string jsonContent = JsonConvert.SerializeObject(user);
            StringContent requisitionBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using(HttpResponseMessage response = await _httpClient.PostAsync("https://notes-cw.azurewebsites.net/NotesAPI/User", requisitionBody))
            {
                
            }
        }

        public static async Task AddNoteAsync(NoteModel note)
        {
            string jsonContent = JsonConvert.SerializeObject(note);
            StringContent requisitionBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using(HttpResponseMessage response = await _httpClient.PostAsync("https://notes-cw.azurewebsites.net/NotesAPI/Note", requisitionBody))
            {
                
            }
        }

        public static async Task UpdateNote(NoteModel note)
        {
            string jsonContent = JsonConvert.SerializeObject(note);
            StringContent requisitionBody = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            using(HttpResponseMessage response = await _httpClient.PutAsync($"https://notes-cw.azurewebsites.net/NotesAPI/Note/{note.Id}", requisitionBody))
            {
                
            }
        }
    }
}