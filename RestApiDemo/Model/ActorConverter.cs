using ActorRepositoryLib;

namespace RestApiDemo.Model
{
    public static class ActorConverter
    {


        public static IActor ActorDTOtoActor(ActorDTO dto)
        {
            IActor actor = new Actor();
            actor.Id = dto.id;
            actor.Name = dto.name;
            actor.BirthYear = dto.birthday;

            return actor;

        }


    }
}
