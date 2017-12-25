#region

using Mono.Game;
using System;
using gameserver.realm;
using gameserver.realm.entity.player;

#endregion

namespace gameserver.logic.behaviors.Drakes
{
    internal class DrakeFollow : CycleBehavior
    {
        protected override void TickCore(Entity host, RealmTime time, ref object state)
        {
            FollowState s;
            if (state == null) s = new FollowState();
            else s = (FollowState)state;

            Status = CycleStatus.NotStarted;

            Player player = host.GetPlayerOwner();
            if (player.Owner == null)
            {
                host.Owner.LeaveWorld(host);
                return;
            }

            Vector2 vect;
            switch (s.State)
            {
                case F.DontKnowWhere:
                    if (s.RemainingTime > 0)
                        s.RemainingTime -= time.ElapsedMsDelta;
                    else
                        s.State = F.Acquired;
                    break;
                case F.Acquired:
                    if (player == null)
                    {
                        s.State = F.DontKnowWhere;
                        s.RemainingTime = 0;
                        break;
                    }
                    if (s.RemainingTime > 0)
                        s.RemainingTime -= time.ElapsedMsDelta;

                    vect = new Vector2(player.X - host.X, player.Y - host.Y);
                    if (vect.Length > 20)
                    {
                        host.Move(player.X, player.Y);
                        host.UpdateCount++;
                    }
                    else if (vect.Length > 1)
                    {
                        float dist = host.GetSpeed(1.2f, time);
                        if (vect.Length > 2)
                            dist = host.GetSpeed(1.2f + ((float)player.Stats[4] / 100), time);
                        else if (vect.Length > 3.5)
                            dist = host.GetSpeed(1.2f + player.Stats[4] + (float)player.Boost[4] / 100, time);
                        else if (vect.Length > 5)
                            dist = host.GetSpeed(1.3f + player.Stats[4] + (float)player.Boost[4] / 100, time);
                        else if (vect.Length > 6)
                            dist = host.GetSpeed(1.4f + player.Stats[4] + (float)player.Boost[4] / 100, time);
                        else if (vect.Length > 7)
                            dist = host.GetSpeed(1.5f + player.Stats[4] + (float)player.Boost[4] / 100, time);

                        Status = CycleStatus.InProgress;
                        vect.X -= Random.Next(-2, 2) / 2f;
                        vect.Y -= Random.Next(-2, 2) / 2f;
                        vect.Normalize();
                        host.ValidateAndMove(host.X + vect.X * dist, host.Y + vect.Y * dist);
                        host.UpdateCount++;
                    }

                    break;
            }

            state = s;
        }

        private enum F
        {
            DontKnowWhere,
            Acquired,
        }

        private class FollowState
        {
            public int RemainingTime;
            public F State;
        }
    }
}
