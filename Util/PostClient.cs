using ServicioAPI.Model;
using System.Text.Json;

namespace ServicioAPI.Util
{

    public class PostClient{
        public HttpClient Client { get; set; }

        public PostClient(HttpClient client)
        {
            this.Client = client;
        }
        public async Task<Post>? GetPost(string id)
        {
            var response = await this.Client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Post>(content);
        }
    public async Task<List<Postid>> GetPostid(string id)
    {
        try
        {
            var response = await this.Client.GetAsync($"https://jsonplaceholder.typicode.com/comments?postId={id}");
            var content = await response.Content.ReadAsStringAsync();

            List<Postid> posts = new List<Postid>();

            using (JsonDocument document = JsonDocument.Parse(content))
        {
            JsonElement root = document.RootElement;
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement element in root.EnumerateArray())
                {
                    Postid post = new Postid
                    {
                        postId = element.GetProperty("postId").GetInt32(),
                        id = element.GetProperty("id").GetInt32(),
                        name = element.GetProperty("name").GetString(),
                        email = element.GetProperty("email").GetString(),
                        body = element.GetProperty("body").GetString()
                    };

                    posts.Add(post);
                }
            }
        }

        return posts;
    }
    catch (Exception ex)
    {
        // Manejar el error o imprimir más detalles sobre el error
        Console.WriteLine(ex);
    }

    return null;
}

    }    
}