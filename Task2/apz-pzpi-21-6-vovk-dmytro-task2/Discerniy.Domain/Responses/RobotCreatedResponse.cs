using Discerniy.Domain.Entity.DomainEntity;

namespace Discerniy.Domain.Responses
{
    public class RobotCreatedResponse
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Token => $"{Id}:{Key}";

        public RobotCreatedResponse(RobotModel model)
        {
            Id = model.Id;
            Key = model.Key;
        }

        public static implicit operator RobotCreatedResponse(RobotModel model)
        {
            return new RobotCreatedResponse(model);
        }
    }
}
