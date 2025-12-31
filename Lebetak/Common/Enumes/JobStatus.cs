namespace Lebetak.Common.Enumes
{
    public enum JobStatus
    {
        Open = 0,
        InProgress = 1,
        Completed = 2,
        Cancelled = 3
    }

    public enum ProposalStatus
    {
        NotChoosed = 0,
        Choosed = 1,
        Waiting = 2,
    }
}

// add job satus enum to represent different states of a job in the system
// update related models and configurations to use the JobStatus enum where applicable
// update post configration 
// create notification model
// Add notification model to App Context

