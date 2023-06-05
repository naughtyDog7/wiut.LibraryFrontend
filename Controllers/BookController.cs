using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryWebApp.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryWebApp.Controllers;

public class BookController : Controller
{

    private readonly HttpClient _client;
    private readonly string ApiUrl;
    private readonly string UserApiUrl;

    public BookController(IConfiguration config)
    {
        var _clientHandler = new HttpClientHandler();
        _clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

        _client = new HttpClient(_clientHandler);
        ApiUrl = config.GetValue<string>("ApiSettings:ApiUrl") + "/books";
        UserApiUrl = config.GetValue<string>("ApiSettings:ApiUrl") + "/users";
    }

    public async Task<IActionResult> Index(int? userId)
    {
        var userResponse = await _client.GetAsync(UserApiUrl);
        if (userResponse.IsSuccessStatusCode)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(await userResponse.Content.ReadAsStringAsync());
            ViewBag.Users = users;
        }
        HttpResponseMessage response;
        if (userId.HasValue)
        {
            response = await _client.GetAsync($"{ApiUrl}?userId={userId.Value}");
        }
        else
        {
            response = await _client.GetAsync(ApiUrl);
        }

        if (response.IsSuccessStatusCode)
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(await response.Content.ReadAsStringAsync());
            return View(books);
        }

        return NotFound();
    }

    public async Task<IActionResult> Create()
    {
        var response = await _client.GetAsync(UserApiUrl);
        if (response.IsSuccessStatusCode)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
            ViewData["UserId"] = new SelectList(users, "Id", "Name");
        }
        return View(new Book());
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync(ApiUrl, content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        var userResponse = await _client.GetAsync(UserApiUrl);
        if (userResponse.IsSuccessStatusCode)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(await userResponse.Content.ReadAsStringAsync());
            ViewData["UserId"] = new SelectList(users, "Id", "Name", book.UserId);
        }
        return View(book);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            var userResponse = await _client.GetAsync(UserApiUrl);
            if (userResponse.IsSuccessStatusCode)
            {
                var users = JsonConvert.DeserializeObject<List<User>>(await userResponse.Content.ReadAsStringAsync());
                ViewData["UserId"] = new SelectList(users, "Id", "Name", book.UserId);
            }
            return View(book);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{ApiUrl}/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(book);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return View(book);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _client.DeleteAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }

    public async Task<IActionResult> Details(int id)
    {
        var response = await _client.GetAsync($"{ApiUrl}/{id}");
        if (response.IsSuccessStatusCode)
        {
            var book = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            return View(book);
        }
        return NotFound();
    }
}
