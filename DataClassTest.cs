using System;

public static class ClientTest
{
    public int id;
    public string name;


    public static List<Client> CreateClients()
    {
        List<Client> clients = new List<Client>() {

            new Client() { id = 1, name = "Petr" },
            new Client() {id = 2, name="Sergii" },
            new Client() {id=3, "Anna" },
            new Client() {id=4, "Ivanka" },
            new Client() {id=5, "Galina"},
            new Client() {id=6, "John Snow"},
            new Client() {id=7, "Lanister"},
            new Client() {id=8, "Vuyko"},
        };
        return clients;
    }
}
