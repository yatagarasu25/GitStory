﻿using ConsoleAppFramework;
using GitStory.Core;
using LibGit2Sharp;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace GitStoryCLI
{
	class Program : ConsoleAppBase
	{
		static string dir;
		static Repository repo;

		static async Task Main(string[] args)
		{
			GitStoryEx.StoryBranchNameDelegate fn = (id, branch, commit) =>
			{
				var m = MethodBase.GetCurrentMethod();
				foreach (var l in m.GetMethodBody().LocalVariables)
				{
					Console.WriteLine(str + " " + l);
				}
			};

			fn("", null, null);

			dir = Repository.Discover(Directory.GetCurrentDirectory());
			using (repo = new Repository(dir))
			{
				await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
			}
		}

		[Command("fix")]
		public async Task Fix()
		{
			repo.Fix();
		}

		[Command("status")]
		public async Task Status()
		{
			repo.Status();
		}

		[Command("get-uuid")]
		public async Task GetUuid()
		{
			Console.WriteLine(repo.GetUuid());
		}

		[Command("set-uuid")]
		public async Task SetUuid([Option(0)] string uuid)
		{
			repo.SetUuid(uuid);
		}

		[Command("change-uuid")]
		public async Task ChangeUuid([Option(0)] string oldUuid)
		{
			repo.ChangeUuid(oldUuid, string.Empty);
		}

		[Command("change-uuid")]
		public async Task ChangeUuid([Option(0)] string oldUuid, [Option(1)] string newUuid)
		{
			repo.ChangeUuid(oldUuid, newUuid);
		}

		[Command("diff")]
		public async Task Diff()
		{
			repo.Diff();
		}

		public void Run()
		{
			repo.Store();
		}
	}
}
