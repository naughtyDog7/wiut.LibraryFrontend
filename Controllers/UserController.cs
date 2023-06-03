using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryWebApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace LibraryWebApp.Controllers;

public class UserController : Controller
{

    private readonly string ApiUrl;
    private HttpClient _client;

    public UserController(IConfiguration config)
    {
        ApiUrl = config.GetValue<string>("ApiSettings:ApiUrl") + "/users";
        var _clientHandler = new HttpClientHandler();
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        _client = new HttpClient(_clientHandler);
    }

    public async Task<IActionResult> Index()
    {
        var response = await _client.GetAsync(ApiUrl);
        if (response.IsSuccessStatusCode)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
            return View(users);
        }
        return NotFound();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        Console.WriteLine("writing " + JsonConvert.SerializeObject(user) + " to " + ApiUrl);
        var response = await _client.PostAsync(ApiUrl, content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return View(user);
        }
        return NotFound();
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return View(user);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, User user)
    {
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{ApiUrl}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(user);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            return View(user);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _client.DeleteAsync($"{ApiUrl}/{id}");
        return RedirectToAction(nameof(Index));
    }
}
