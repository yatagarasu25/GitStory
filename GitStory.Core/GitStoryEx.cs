﻿using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemEx;

namespace GitStory.Core
{
	public static class GitStoryEx
	{
		public static void Store(this Repository repo)
		{
			var head = repo.Head;
			var lastHeadCommit = repo.Head.Commits.First();

			var storyBranchName = $"{head.FriendlyName}_{lastHeadCommit.Sha}_changes";
			var diaryBranch = repo.Branches.Where(b => b.FriendlyName == storyBranchName).FirstOrDefault();

			diaryBranch = diaryBranch ?? repo.CreateBranch(storyBranchName);

			var headRef = repo.Refs.Where(r => r.CanonicalName == head.CanonicalName).FirstOrDefault();
			var oldHeadRef = headRef;

			var diaryBranchRef = repo.Refs.Where(r => r.CanonicalName == diaryBranch.CanonicalName).FirstOrDefault();

			// got branches

			List<string> filesNotStaged = new List<string>();

			foreach (var item in repo.RetrieveStatus(new StatusOptions { ExcludeSubmodules = true, IncludeIgnored = false }))
			{
				if (!item.State.HasFlag(FileStatus.ModifiedInIndex))
				{
					filesNotStaged.Add(item.FilePath);
				}
			}
			repo.Refs.UpdateTarget("HEAD", diaryBranchRef.CanonicalName);

			// saved HEAD

			Commands.Stage(repo, "*");

			try
			{
				var author = new Signature(
					new Identity(repo.Config.Get<string>("user.name").Value, repo.Config.Get<string>("user.email").Value)
					, DateTime.Now);
				repo.Commit("update", author, author);
			}
			catch (Exception e)
			{
			}

			// restore HEAD

			repo.Refs.UpdateTarget("HEAD", oldHeadRef.CanonicalName);

			Commands.Unstage(repo, filesNotStaged);
		}

		public static void Fix(this Repository repo)
		{
			var diaryBranch = repo.Head;

			var headBranchName = diaryBranch.CanonicalName.CutEnd('_').CutEnd('_');
			var headBranch = repo.Branches.Where(b => b.CanonicalName == headBranchName).FirstOrDefault();

			repo.Refs.UpdateTarget("HEAD", headBranch.CanonicalName);
		}
	}
}