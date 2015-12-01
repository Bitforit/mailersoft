<?php
	
	namespace MailerLite;

	use MailerLite\Base\Rest;
	
	class Lists extends Rest
	{
		function Lists( $api_key )
		{	
			$this->name = 'lists';

			parent::__construct($api_key);
		}

		function getActive( )
		{
			$this->path .= 'active/';

			return $this->execute( 'GET' );
		}

		function getUnsubscribed( )
		{
			$this->path .= 'unsubscribed/';

			return $this->execute( 'GET' );
		}

		function getBounced( )
		{			
			$this->path .= 'bounced/';

			return $this->execute( 'GET' );
		}
	}

?>