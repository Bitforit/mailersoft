<?php

require_once dirname(__FILE__).'/base/ML_Rest.php';

class ML_Campaigns extends ML_Rest
{
    var $tag = null;
    var $date_from = null;
    var $date_to = null;
    var $transactional = null;

    function ML_Campaigns( $api_key )
    {
        $this->name = 'campaigns';

        parent::__construct($api_key);
    }

    function get( $data = null )
    {
        foreach (array('tag') as $param)
        {
            if (is_null($data[$param]) && !is_null($this->{$param}))
            {
                $data[$param] = $this->{$param};
            }
        }

        return parent::get($data);
    }

    function getAll( $data = null )
    {
        foreach (array('transactional', 'date_from', 'date_to') as $param)
        {
            if (!isset($data[$param]) &&
                !is_null($this->{$param}))
            {
                $data[$param] = $this->{$param};
            }
        }

        return parent::getAll($data);
    }

    function getRecipients( $data = null )
    {
        foreach (array('tag', 'date_from', 'date_to') as $param)
        {
            if (empty($data[$param]) && !empty($this->{$param}))
            {
                $data[$param] = $this->{$param};
            }
        }

        return $this->execute( 'GET', $data, 'recipients' );
    }

    function getOpens( $data = null )
    {
        foreach (array('tag') as $param)
        {
            if (empty($data[$param]) && !empty($this->{$param}))
            {
                $data[$param] = $this->{$param};
            }
        }
        return $this->execute( 'GET', $data, 'opens' );
    }

    function getClicks( $data = null )
    {
        foreach (array('tag') as $param)
        {
            if (empty($data[$param]) && !empty($this->{$param}))
            {
                $data[$param] = $this->{$param};
            }
        }
        return $this->execute( 'GET', $data, 'clicks' );
    }

    function getUnsubscribes( $data = null )
    {
        return $this->execute( 'GET', $data, 'unsubscribes' );
    }

    function getBounces( $data = null )
    {
        return $this->execute( 'GET', $data, 'bounces' );
    }

    function getJunk( $data = null )
    {
        return $this->execute( 'GET', $data, 'junks' );
    }

    function setTag( $tag = null )
    {
        $this->tag = $tag;
        return $this;
    }

    function getTags( $data = null )
    {
        return $this->execute( 'GET', $data, 'tags' );
    }

    function setDateFrom( $date = null )
    {
        $this->date_from = $date;
        return $this;
    }

    function setDateTo( $date = null )
    {
        $this->date_to = $date;
        return $this;
    }

    function getRecipientData( $data = null )
    {
        return $this->execute( 'GET', $data, 'recipient' );
    }

    function setTransactional( $transactional = false )
    {
        $this->transactional = $transactional;
        return $this;
    }
}

?>