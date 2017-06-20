namespace VkNet.Model
{
    using System.Collections.ObjectModel;

    using Utils;

    /// <summary>
    /// Информация о беседе (мультидиалоге, чате).
    /// См. описание http://vk.com/dev/chat_object
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Идентификатор беседы.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Тип диалога.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Название беседы.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Путь к аватару 200x200
        /// </summary>
        public string Photo200 { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который является создателем беседы.
        /// </summary>
        public long? AdminId { get; set; }

        /// <summary>
        /// Список идентификаторов участников беседы.
        /// </summary>
        public Collection<long> Users { get; set; }

        #region Поля найденые эксперементально

        /// <summary>
        /// Состоит ли аккаунт в беседе или покинул ее
        /// </summary>
        public bool Left { get; set; }

        /// <summary>
        /// Был ли аккаунт выкинут из беседы
        /// </summary>
        public bool Kicked { get; set; }
        /// <summary>
        /// Неизвестно что за поле, но оно есть в некоторых диалогах (Вроде не влияет на звоковое уведомление о новых сообщениях)
        /// </summary>
        public bool? Sound { get; set; }
        /// <summary>
        /// Неизвестно что за поле, но оно есть в некоторых диалогах (При отключенных звуковых уведомлениях равняеться -1)
        /// </summary>
        public int? DisabledUntil { get; set; }

		#endregion

		#region Методы
		/// <summary>
		/// Разобрать из json.
		/// </summary>
		/// <param name="response">Ответ сервера.</param>
		/// <returns></returns>
		public static Chat FromJson(VkResponse response)
		{
			var chat = new Chat
			{
				Id = response["id"],
				Type = response["type"],
				Title = response["title"],
			    Photo200 = response["photo_200"],
                AdminId = Utilities.GetNullableLongId(response["admin_id"]),
				Users = response["users"],

				#region Поля найденые эксперементально

				Left = response.ContainsKey("left") && response["left"],
				Kicked = response.ContainsKey("kicked") && response["kicked"]
			};
			if (response.ContainsKey("push_settings"))
			{
				chat.Sound = response["push_settings"]["sound"];
				chat.DisabledUntil = response["push_settings"]["disabled_until"];
			} else
			{
				chat.Sound = null;
				chat.DisabledUntil = null;
			}
			#endregion

			return chat;
		}

		#endregion
	}
}