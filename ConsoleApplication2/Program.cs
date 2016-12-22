using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using Octokit;
using LibGit2Sharp;
using System.IO;

namespace garbageman
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("garbageman-package-manager"));
            var issues = client.Issue.GetAllForRepository("faso", "garbageman").Result.Select(o => new { o.Title, o.Body }).ToList();

            var co = new CloneOptions();
            co.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = "login", Password = "password" };
            try {
                var url = issues.SingleOrDefault(o => o.Title == args[0]).Body;
                var name = issues.SingleOrDefault(o => o.Title == args[0]).Title;
                LibGit2Sharp.Repository.Clone(url, Path.Combine(Environment.CurrentDirectory, "garbage", name), co);
            }
            catch
            {
                Console.WriteLine("fuck you");
            }
        }
    }
}
