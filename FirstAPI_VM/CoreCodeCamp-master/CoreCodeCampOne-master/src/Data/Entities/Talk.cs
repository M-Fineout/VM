namespace CoreCodeCamp.Data
{
    public class Talk
    {
        public int TalkId { get; set; }
        public Camp Camp { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int Level { get; set; }

        //this name has to be the same as the name of the SpeakerModel in TalkModel
        public Speaker Speaker { get; set; }
    }
}