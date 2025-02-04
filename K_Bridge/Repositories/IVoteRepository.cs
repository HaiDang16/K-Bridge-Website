﻿using K_Bridge.Models;

namespace K_Bridge.Repositories
{
    public interface IVoteRepository
    {
        IQueryable<Vote> Votes { get; }
        void SaveVote(Vote vote);
        Vote GetVoteById(int id);
        List<UserVote>? GetUserVotesListById(int id, Vote vote);
        void RemoveAllVote(List<UserVote> vote);
        VoteOption GetVoteOptionById(int id);
        void SaveUserVote(UserVote vote);
        void IncreaseOneVoteCount(VoteOption vote);
        List<VoteOption> GetVoteOptionsByVoteId(int voteId);
        List<Vote> GetVotesByPostId(int postId);
        List<UserVote> GetUserVoteForPost(int userId, int postId);
        void RemoveUserVote(UserVote userVote);
        void DecreaseOneVoteCount(VoteOption voteOption);
        Vote GetVoteWithOptionByPostId(int id);
        int CountUserVoteById(int id);
        void UpdateVote(Vote vote);
        IEnumerable<Vote> GetOpenVotes();
        void RemoveVoteOption(VoteOption voteOption);
        void SaveVoteOption(VoteOption vote);
        void RemoveVote(Vote vote);
    }
}
